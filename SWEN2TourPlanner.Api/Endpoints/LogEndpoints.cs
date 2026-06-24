using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SWEN2TourPlanner.Api.Dtos;
using SWEN2TourPlanner.Bll;

namespace SWEN2TourPlanner.Api.Endpoints;

public static class LogEndpoints
{
    public static void MapLogEndpoints(this IEndpointRouteBuilder app)
    {
        var baseGroup = app.MapGroup("/api");
        var group = baseGroup.MapGroup("/tours/{tourId}/logs");
        var logs = baseGroup.MapGroup("/logs");

        group.MapGet("/", GetAllLogsForTour);
        logs.MapPost("/", CreateLog);
        logs.MapGet("/{logId}", GetLogById);
        logs.MapPut("/{logId}", UpdateLog);
        logs.MapDelete("/{logId}", DeleteLog);
    }

    private static async Task<IEnumerable<LogDto>> GetAllLogsForTour([FromQuery] string? search, int tourId, ILogService logService,
        ClaimsPrincipal user)
    {
        var username = user.Identity!.Name!;
        return (await logService.FindMatchingLogsAsync(username, tourId, search)).Select(l => l.ToDto());
    }

    private static async Task<Results<Created<LogDto>, Conflict<string>, InternalServerError<string>>> CreateLog(
        [FromBody] LogDto logDto, ILogService logService, ClaimsPrincipal user)
    {
        var username = user.Identity!.Name!;
        try
        {
            var createdLog = (await logService.CreateLogAsync(logDto.ToLog())).ToDto();
            string? uri = null; // TODO: generate URI for the created resource
            return TypedResults.Created(uri, createdLog);
        }
        catch (LogAlreadyExistsException e)
        {
            return TypedResults.Conflict(e.Message);
        }
        catch (Exception e)
        {
            return TypedResults.InternalServerError(e.Message);
        }
    }

    private static async Task<Results<Ok<LogDto>, NotFound<string>>> GetLogById(int logId, ILogService logService,
        ClaimsPrincipal user)
    {
        var username = user.Identity!.Name!;
        var log = await logService.GetLogAsync(logId);
        if (log == null) return TypedResults.NotFound("Log not found");
        return TypedResults.Ok(log.ToDto());
    }

    private static async Task<Results<NoContent, NotFound<string>, InternalServerError<string>>> UpdateLog(int logId,
        [FromBody] LogDto logDto, ILogService logService, ClaimsPrincipal user)
    {
        var username = user.Identity!.Name!;
        try
        {
            var logToUpdate = logDto.ToLog();
            logToUpdate.Id = logId;
            var success = await logService.UpdateLogAsync(logToUpdate);
            if (!success) return TypedResults.NotFound("Log not found");
            return TypedResults.NoContent();
        }
        catch (Exception e)
        {
            return TypedResults.InternalServerError(e.Message);
        }
    }

    private static async Task<Results<NoContent, NotFound<string>, InternalServerError<string>>> DeleteLog(int logId,
        ILogService logService, ClaimsPrincipal user)
    {
        var username = user.Identity!.Name!;
        try
        {
            var success = await logService.RemoveLogAsync(logId);
            if (!success) return TypedResults.NotFound("Log not found");
            return TypedResults.NoContent();
        }
        catch (Exception e)
        {
            return TypedResults.InternalServerError(e.Message);
        }
    }
}