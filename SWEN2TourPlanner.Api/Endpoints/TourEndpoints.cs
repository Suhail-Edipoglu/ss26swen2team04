using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SWEN2TourPlanner.Api.Dtos;
using SWEN2TourPlanner.Bll;

namespace SWEN2TourPlanner.Api.Endpoints;

public static class TourEndpoints
{
    public static void MapTourEndpoint(this IEndpointRouteBuilder app)
    {
        var baseGroup = app.MapGroup("/api");
        var group = baseGroup.MapGroup("/tours").RequireAuthorization();

        group.MapGet("/", GetAllTours);
        group.MapPost("/", CreateTour).AddEndpointFilter<ValidationFilter<TourDto>>();
        group.MapGet("/{id}", GetTourById);
        group.MapPut("/{id}", UpdateTour).AddEndpointFilter<ValidationFilter<TourDto>>();
        group.MapDelete("/{id}", DeleteTour);
        group.MapGet("/export", ExportTours);
        group.MapPost("/import", ImportTours);
    }

    private static async Task<Results<Ok<List<TourDto>>, NotFound<string>>> GetAllTours([FromQuery] string? search, ITourService tourService, ClaimsPrincipal user)
    {
        var username = user.Identity!.Name!;
        var tours = ((await tourService.FindMatchingToursAsync(username, search)).Select(t => t.ToDto())).ToList(); // todo
        if (tours == null || !tours.Any()) return TypedResults.NotFound("No tours found");
        return TypedResults.Ok(tours);
    }

    private static async Task<Results<Created<int>, InternalServerError<string>>> CreateTour(
        [FromBody] TourDto tourDto, ITourService tourService, ClaimsPrincipal user)
    {
        var username = user.Identity!.Name!;
        try
        {
            var tour = tourDto.ToTour(); // todo dtos?
            var createdTour = (await tourService.CreateTourAsync(tour, username)).ToDto();
            string? uri = null; // TODO: generate URI for the created resource
            return TypedResults.Created(uri, createdTour.Id);
        }
        catch (Exception e)
        {
            return TypedResults.InternalServerError(e.Message);
        }
    }

    private static async Task<Results<Ok<TourDto>, NotFound<string>>> GetTourById(int id, ITourService tourService,
        ClaimsPrincipal user)
    {
        var username = user.Identity!.Name!;
        var tour = await tourService.GetTourAsync(username, id);
        if (tour == null || tour.User.Username != username) return TypedResults.NotFound("Tour not found");
        return TypedResults.Ok(tour.ToDto());
    }

    private static async Task<Results<Ok<bool>, NotFound<string>, InternalServerError<string>>> UpdateTour(int id,
        [FromBody] TourDto tourDto, ITourService tourService, ClaimsPrincipal user)
    {
        var username = user.Identity!.Name!;
        var tour = await tourService.GetTourAsync(username, id);
        if (tour == null || tour.User.Username != username) return TypedResults.NotFound("Tour not found");
        try
        {
            var usertmp = tour.User;
            tour = tourDto.ToTour();
            tour.User = usertmp;
            var success = await tourService.UpdateTourAsync(id, tour);
            if (!success) return TypedResults.NotFound("Tour not found");
            return TypedResults.Ok(success);
        }
        catch (Exception e)
        {
            return TypedResults.InternalServerError(e.Message);
        }
    }

    private static async Task<Results<Ok<bool>, NotFound<string>, InternalServerError<string>>> DeleteTour(int id,
        ITourService tourService, ClaimsPrincipal user)
    {
        var username = user.Identity!.Name!;
        try
        {
            var success = await tourService.RemoveTourAsync(username, id);
            if (!success) return TypedResults.NotFound("Tour not found");
            return TypedResults.Ok(success);
        }
        catch (Exception e)
        {
            return TypedResults.InternalServerError(e.Message);
        }
    }
    
    private static async Task<Results<Ok<List<TourDto>>, InternalServerError<string>>> ExportTours(ITourService tourService, ClaimsPrincipal user)
    {
        var username = user.Identity!.Name!;
        try
        {
            var tours = (await tourService.ExportToursAsync(username)).Select(t => t.ToDto()).ToList();
            return TypedResults.Ok(tours);
        }
        catch (Exception e)
        {
            return TypedResults.InternalServerError(e.Message);
        }
    }
    
    private static async Task<Results<Ok<bool>, InternalServerError<string>>> ImportTours([FromBody] List<TourDto> tourDtos, ITourService tourService, ClaimsPrincipal user)
    {
        var username = user.Identity!.Name!;
        try
        {
            var tours = tourDtos.Select(tourDto => tourDto.ToTour()).ToList();
            var success = (await tourService.ImportToursAsync(username, tours));
            if (!success) return TypedResults.InternalServerError("Failed to import tours");
            return TypedResults.Ok(success);
        }
        catch (Exception e)
        {
            return TypedResults.InternalServerError(e.Message);
        }
    }
}