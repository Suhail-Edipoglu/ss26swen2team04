using SWEN2TourPlanner.Frontend.Components;
using SWEN2TourPlanner.Frontend.ViewModels;
using SWEN2TourPlanner.Frontend.ViewModels.Interfaces;
using Blazing.Mvvm;
using SWEN2TourPlanner.Frontend.Services;
using SWEN2TourPlanner.Frontend.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Wire Models
builder.Services.AddScoped<IApiService, CachedApiService>();
// builder.Services.AddScoped<IApiService, ApiService>();
builder.Services.AddSingleton<IMapService, LeafletMapService>();

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
