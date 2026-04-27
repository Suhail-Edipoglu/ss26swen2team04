using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SWEN2TourPlanner.Api.Dtos;

namespace SWEN2TourPlanner.Api.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoint(this IEndpointRouteBuilder app)
    {
        var baseGroup = app.MapGroup("/api");
        var group = baseGroup.MapGroup("/auth");

        group.MapPost("/register", Register);
        
        var summaries = new[] {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        baseGroup.MapGet("/weatherforecast", () => {
                var forecast = Enumerable.Range(1, 5).Select(index =>
                        new WeatherForecast
                        (
                            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                            Random.Shared.Next(-20, 55),
                            summaries[Random.Shared.Next(summaries.Length)]
                        ))
                    .ToArray();
                return forecast;
            })
            .WithName("GetWeatherForecast");
    }

    public static void Register([FromBody] CredentialsDto credentials)
    {
        Console.WriteLine($"Received registration request for username: {credentials.Username}");
        Console.WriteLine($"Password: {credentials.Password}");
    }
}

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary) {
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}