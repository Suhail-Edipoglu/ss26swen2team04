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
builder.Services.AddHttpClient<ApiService>(client =>
{
    client.BaseAddress = new Uri("https://api.example.com/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});
string openRouteServiceKey = "none"; // TODO Add api key and stuff
builder.Services.AddHttpClient<OpenRouteServiceService>(client =>
{
    client.BaseAddress = new Uri("https://api.openrouteservice.org/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.DefaultRequestHeaders.Add("Authorization", openRouteServiceKey);
});

// Wire Models
builder.Services.AddSingleton<IApiService, CachedApiService>();
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
