using Blazing.Mvvm.ComponentModel;
using Blazing.Mvvm.Components;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SWEN2TourPlanner.Frontend.DTOs;
using SWEN2TourPlanner.Frontend.DTOs.Enums;
using SWEN2TourPlanner.Frontend.DTOs.Messages;
using SWEN2TourPlanner.Frontend.Models.Interfaces;
using SWEN2TourPlanner.Frontend.Services.Interfaces;
using SWEN2TourPlanner.Frontend.ViewModels.Interfaces;

namespace SWEN2TourPlanner.Frontend.ViewModels;

[ViewModelDefinition<IHomeViewModel>]
public sealed partial class HomeViewModel(IApiService apiService, ICache cache, IMvvmNavigationManager mvvmNavigationManager, ILoggerFactory loggerFactory) : ViewModelBase, IHomeViewModel {
    private readonly IApiService _apiService = apiService;
    private readonly ICache _cache = cache;
    private readonly IMvvmNavigationManager _mvvmNavigationManager = mvvmNavigationManager;
    private readonly ILogger _logger = loggerFactory.CreateLogger("HomeViewModel");
    
    [ObservableProperty]
    private List<Tour> _tours = [];

    [ObservableProperty]
    private string _tourSearchTerm = string.Empty;

    public override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        WeakReferenceMessenger.Default.Send(new NavbarStateChangedMessage(NavbarState.Main));
        await LoadToursAsync();
    }

    [RelayCommand]
    private void OpenTour(Tour tour) {
        _cache.CurrentTour = CloneTour(tour);
        _cache.CurrentTourLog = null;
        _mvvmNavigationManager.NavigateTo<ITourViewModel>();
    }

    private async Task LoadToursAsync() {
        var tours = string.IsNullOrWhiteSpace(TourSearchTerm)
            ? await _apiService.GetToursAsync()
            : await _apiService.SearchToursAsync(TourSearchTerm);

        Tours = tours.OrderBy(t => t.Name).ToList();
        _logger.LogInformation("Loaded {Count} tours from API", Tours.Count);
    }

    [RelayCommand]
    private async Task UpdateSearchResults() {
        await LoadToursAsync();
    }


    private static Tour CloneTour(Tour source) => new() {
        Id = source.Id,
        Name = source.Name,
        Description = source.Description,
        From = source.From,
        To = source.To,
        TransportType = source.TransportType,
        Distance = source.Distance,
        EstimatedTime = source.EstimatedTime,
        RouteInformation = source.RouteInformation,
        UserId = source.UserId,
        Popularity = source.Popularity,
        ChildFriendliness = source.ChildFriendliness,
    };
    
    [RelayCommand]
    private void CreateTour() {
        _cache.CurrentTour = null;
        _cache.CurrentTourLog = null;
        _mvvmNavigationManager.NavigateTo<ITourViewModel>();
    }
}