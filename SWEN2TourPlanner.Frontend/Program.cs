using SWEN2TourPlanner.Frontend.Components;
using SWEN2TourPlanner.Frontend.ViewModels;
using SWEN2TourPlanner.Frontend.ViewModels.Interfaces;
using Blazing.Mvvm;
using SWEN2TourPlanner.Frontend.Services;
using SWEN2TourPlanner.Frontend.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Wire View Models
builder.Services.AddScoped<ICurrentTourLogViewModel, CurrentTourLogViewModel>();
builder.Services.AddScoped<ICurrentTourViewModel, CurrentTourViewModel>();
builder.Services.AddScoped<IMapViewModel, MapViewModel>();
builder.Services.AddSingleton<INavMenuViewModel, NavMenuViewModel>();
builder.Services.AddSingleton<ITourService, CachedTourService>();

builder.Services.AddMvvm(options =>
{ 
    options.HostingModelType = BlazorHostingModelType.Server;
});

// Wire Models (Not yet Implemented)
// builder.Services.AddSingleton<IMapService, MapService>();
// builder.Services.AddSingleton<IApiService, ApiService>();

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
