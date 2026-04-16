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
builder.Services.AddScoped<IMapViewModel, MapViewModel>();
builder.Services.AddScoped<ICurrentTourViewModel, CurrentTourViewModel>();
builder.Services.AddScoped<ICurrentTourLogViewModel, CurrentTourLogViewModel>();
builder.Services.AddScoped<ITourListViewModel, TourListViewModel>();
builder.Services.AddSingleton<INavMenuViewModel, NavMenuViewModel>();

// Wire Models
builder.Services.AddSingleton<ITourService, CachedTourService>();
builder.Services.AddSingleton<ILogService, CachedLogService>();

builder.Services.AddMvvm(options =>
{ 
    options.HostingModelType = BlazorHostingModelType.Server;
});

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
