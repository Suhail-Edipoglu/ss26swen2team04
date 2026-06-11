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