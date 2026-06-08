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
        var group = baseGroup.MapGroup("/tours");

        group.MapGet("/", GetAllTours);
        group.MapPost("/", CreateTour);
        group.MapGet("/{id}", GetTourById);
        group.MapPut("/{id}", UpdateTour);
        group.MapDelete("/{id}", DeleteTour);
    }

    private static async Task<IEnumerable<TourDto>> GetAllTours(ITourService tourService, ClaimsPrincipal user)
    {
        var username = user.Identity!.Name!;
        return (await tourService.FindMatchingToursAsync(username)).Select(t => t.ToDto());
    }

    private static async Task<Results<Created<TourDto>, Conflict<string>, InternalServerError<string>>> CreateTour(
        [FromBody] TourDto tourDto, ITourService tourService, ClaimsPrincipal user)
    {
        var username = user.Identity!.Name!;
        try
        {
            var createdTour = (await tourService.CreateTourAsync(tourDto.ToTour())).ToDto();
            string? uri = null; // TODO: generate URI for the created resource
            return TypedResults.Created(uri, createdTour);
        }
        catch (TourAlreadyExistsException e)
        {
            return TypedResults.Conflict(e.Message);
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
        var tour = await tourService.GetTourAsync(id);
        if (tour == null || tour.Name != username) return TypedResults.NotFound("Tour not found");
        return TypedResults.Ok(tour.ToDto());
    }

    private static async Task<Results<NoContent, NotFound<string>, InternalServerError<string>>> UpdateTour(int id,
        [FromBody] TourDto tourDto, ITourService tourService, ClaimsPrincipal user)
    {
        var username = user.Identity!.Name!;
        try
        {
            var success = await tourService.UpdateTourAsync(id, tourDto.ToTour());
            if (!success) return TypedResults.NotFound("Tour not found");
            return TypedResults.NoContent();
        }
        catch (Exception e)
        {
            return TypedResults.InternalServerError(e.Message);
        }
    }

    private static async Task<Results<NoContent, NotFound<string>, InternalServerError<string>>> DeleteTour(int id,
        ITourService tourService, ClaimsPrincipal user)
    {
        var username = user.Identity!.Name!;
        try
        {
            var success = await tourService.RemoveTourAsync(id);
            if (!success) return TypedResults.NotFound("Tour not found");
            return TypedResults.NoContent();
        }
        catch (Exception e)
        {
            return TypedResults.InternalServerError(e.Message);
        }
    }
}