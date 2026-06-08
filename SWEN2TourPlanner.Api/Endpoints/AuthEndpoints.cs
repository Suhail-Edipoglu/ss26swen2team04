using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SWEN2TourPlanner.Api.Dtos;
using SWEN2TourPlanner.Api.Services;
using SWEN2TourPlanner.Bll;
using SWEN2TourPlanner.Bll.Exceptions;

namespace SWEN2TourPlanner.Api.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoint(this IEndpointRouteBuilder app)
    {
        var baseGroup = app.MapGroup("/api");
        var group = baseGroup.MapGroup("/auth");

        group.MapPost("/register", Register);
        group.MapPost("/login", Login);

        //
        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        baseGroup.MapGet("/weatherforecast", () =>
            {
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
    //

    public static async Task<Results<Created, Conflict<string>>> Register([FromBody] CredentialsDto credentials,
        IUserService userService, IPasswordHashingService passwordHashingService, ITokenService tokenService)
    {
        // debug
        Console.WriteLine($"Received registration request for username: {credentials.Username}");
        Console.WriteLine($"Password: {credentials.Password}");

        var hashedPassword = passwordHashingService.Hash(credentials.Password);
        try
        {
            var user = await userService.RegisterUserAsync(credentials.Username, hashedPassword);
            return TypedResults.Created();
        }
        catch (UserAlreadyExistsException)
        {
            return TypedResults.Conflict("Username already exists");
        }
    }

    public static async Task<Results<Ok<TokenDto>, UnauthorizedHttpResult>> Login([FromBody] CredentialsDto credentials,
        IUserService userService, IPasswordHashingService passwordHashingService, ITokenService tokenService)
    {
        // debug
        Console.WriteLine($"Received login request for username: {credentials.Username}");
        Console.WriteLine($"Password: {credentials.Password}");

        try
        {
            var user = await userService.GetUserByUsernameAsync(credentials.Username);

            var isPasswordValid = passwordHashingService.Verify(user.HashedPassword, credentials.Password);
            if (!isPasswordValid) throw new InvalidDataException("Invalid credentials");

            var token = new TokenDto { Token = tokenService.GenerateToken(user) };
            return TypedResults.Ok(token);
        }
        catch (Exception e) when (e is UserNotFoundException || e is InvalidDataException)
        {
            return TypedResults.Unauthorized();
        }
    }
}

//
internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
//