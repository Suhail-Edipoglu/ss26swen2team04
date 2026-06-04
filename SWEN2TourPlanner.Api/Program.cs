using SWEN2TourPlanner.Api.Endpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SWEN2TourPlanner.Api.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

// todo launchsettings localhost

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

var jwtSection = builder.Configuration.GetSection(JwtOptions.Jwt);
builder.Services
    .AddOptions<JwtOptions>()
    .Bind(jwtSection)
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtOptions = jwtSection.Get<JwtOptions>()!;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtOptions.Issuer,
            ValidAudience = jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapAuthEndpoint();
app.MapTourEndpoint();
app.MapLogEndpoints();


app.Run();
