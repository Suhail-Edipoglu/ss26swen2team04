using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SWEN2TourPlanner.Api.Endpoints;
using SWEN2TourPlanner.Api.Exceptions;
using SWEN2TourPlanner.Api.Services;
using SWEN2TourPlanner.Bll;
using SWEN2TourPlanner.Dal;

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

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddScoped<IPasswordHashingService, PasswordHashingService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITourService, TourService>();
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<IUserRepository, EntityFrameworkUserRepository>();
builder.Services.AddScoped<ITourRepository, EntityFrameworkTourRepository>();
builder.Services.AddScoped<ILogRepository, EntityFrameworkLogRepository>();
builder.Services.AddDbContext<SWEN2TourPlannerDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")!);
    if (builder.Environment.IsDevelopment())
    {
        options.EnableSensitiveDataLogging();
        options.LogTo(Console.WriteLine, LogLevel.Information);
    }
});

builder.Services.AddCors();

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
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseCors(policy =>
    {
        // todo map to frontend server policy.WithOrigins("http://localhost:4200")
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        
        var context = services.GetRequiredService<SWEN2TourPlannerDbContext>();
        context.Database.EnsureCreated();
        context.Database.Migrate();
    }
}

app.UseExceptionHandler(options => { });

app.UseAuthentication();
app.UseAuthorization();

app.MapAuthEndpoint();
app.MapTourEndpoint();
app.MapLogEndpoints();


app.Run();