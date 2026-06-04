namespace SWEN2TourPlanner.Api.Endpoints;

public static class LogEndpoints
{
    public static void MapLogEndpoints(this IEndpointRouteBuilder app)
    {
        var baseGroup = app.MapGroup("/api");
        var group = baseGroup.MapGroup("/tours/{tourId}/logs");
        var logs = baseGroup.MapGroup("/logs/{logId}");
        
        group.MapGet("/", GetAllLogsForTour);
        group.MapPost("/", CreateLogForTour);
        logs.MapPut("/", UpdateLog);
        logs.MapDelete("/", DeleteLog);
    }
}