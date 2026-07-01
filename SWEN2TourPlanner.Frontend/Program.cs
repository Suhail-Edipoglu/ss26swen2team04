using SWEN2TourPlanner.Frontend.Components;
using Blazing.Mvvm;
using MudBlazor.Services;
using SWEN2TourPlanner.Frontend.Models;
using SWEN2TourPlanner.Frontend.Models.Interfaces;
using SWEN2TourPlanner.Frontend.Services;
using SWEN2TourPlanner.Frontend.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents(); // Use Blazor Server Rendering
builder.Services.AddMudServices(); // MudBlazor

// Add Service APIs
var apiBaseAddress = builder.Configuration["ApiService:BaseAddress"]
    ?? throw new InvalidOperationException("ApiService:BaseAddress is not configured.");
var orsBaseAddress = builder.Configuration["OpenRouteService:BaseAddress"]
    ?? throw new InvalidOperationException("OpenRouteService:BaseAddress is not configured.");
var orsApiKey = builder.Configuration["OpenRouteService:ApiKey"] ?? string.Empty;

builder.Services.AddHttpClient<IApiService, ApiService>(client =>
{
    client.BaseAddress = new Uri(apiBaseAddress);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});
builder.Services.AddHttpClient<IAuthApiService, AuthApiService>(client =>
{
    client.BaseAddress = new Uri(apiBaseAddress);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});
builder.Services.AddHttpClient<IRouteApiService, OpenRouteServiceService>(client =>
{
    client.BaseAddress = new Uri(orsBaseAddress);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    if (!string.IsNullOrWhiteSpace(orsApiKey)) {
        client.DefaultRequestHeaders.Add("Authorization", orsApiKey);
    }
});

// Wire Models
builder.Services.AddScoped<ILoginManager, LoginManager>();
builder.Services.AddScoped<ICache, Cache>();

// Add Blazing MVVM, this also enables ViewModel Loading
builder.Services.AddMvvm(options =>
{
    options.HostingModelType = BlazorHostingModelType.Server;
    options.ParameterResolutionMode = ParameterResolutionMode.ViewModel;
});
// Add Http Client for Api Calls
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}
app.UseStatusCodePagesWithReExecute("/", createScopeForStatusCodePages: true);
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
