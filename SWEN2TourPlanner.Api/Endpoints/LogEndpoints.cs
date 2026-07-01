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
        var group = baseGroup.MapGroup("/tours/{tourId}/logs").RequireAuthorization();
        var logs = baseGroup.MapGroup("/logs").RequireAuthorization();

        group.MapGet("/", GetAllLogsForTour);
        logs.MapPost("/", CreateLog);
        logs.MapGet("/{logId}", GetLogById);
        logs.MapPut("/{logId}", UpdateLog);
        logs.MapDelete("/{logId}", DeleteLog);
    }

    private static async Task<Results<Ok<List<LogDto>>, NotFound<string>>> GetAllLogsForTour([FromQuery] string? search, int tourId, ILogService logService,
        ClaimsPrincipal user)
    {
        var username = user.Identity!.Name!;
        var logs = (await logService.FindMatchingLogsAsync(username, tourId, search)).Select(l => l.ToDto()).ToList();
        if (logs == null || !logs.Any()) return TypedResults.NotFound("No logs found");
        return TypedResults.Ok(logs);
    }

    private static async Task<Results<Created<int>, Conflict<string>, InternalServerError<string>>> CreateLog(
        [FromBody] LogDto logDto, ILogService logService, ClaimsPrincipal user)
    {
        var username = user.Identity!.Name!;
        try
        {
            var createdLog = (await logService.CreateLogAsync(logDto.ToLog(), username)).ToDto();
            string? uri = null; // TODO: generate URI for the created resource
            return TypedResults.Created(uri, createdLog.Id);
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
        var log = await logService.GetLogAsync(logId, username);
        if (log == null) return TypedResults.NotFound("Log not found");
        return TypedResults.Ok(log.ToDto());
    }

    private static async Task<Results<Ok<bool>, NotFound<string>, InternalServerError<string>>> UpdateLog(int logId,
        [FromBody] LogDto logDto, ILogService logService, ClaimsPrincipal user)
    {
        var username = user.Identity!.Name!;
        try
        {
            var logToUpdate = logDto.ToLog();
            logToUpdate.Id = logId;
            var success = await logService.UpdateLogAsync(logToUpdate, username);
            if (!success) return TypedResults.NotFound("Log not found");
            return TypedResults.Ok(success);
        }
        catch (Exception e)
        {
            return TypedResults.InternalServerError(e.Message);
        }
    }

    private static async Task<Results<Ok<bool>, NotFound<string>, InternalServerError<string>>> DeleteLog(int logId,
        ILogService logService, ClaimsPrincipal user)
    {
        var username = user.Identity!.Name!;
        try
        {
            var success = await logService.RemoveLogAsync(logId, username);
            if (!success) return TypedResults.NotFound("Log not found");
            return TypedResults.Ok(success);
        }
        catch (Exception e)
        {
            return TypedResults.InternalServerError(e.Message);
        }
    }
}