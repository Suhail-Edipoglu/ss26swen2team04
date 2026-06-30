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
public sealed partial class HomeViewModel(IApiService apiService, ICache cache, IMvvmNavigationManager mvvmNavigationManager) : ViewModelBase, IHomeViewModel {
    private readonly IApiService _apiService = apiService;
    private readonly ICache _cache = cache;
    private readonly IMvvmNavigationManager _mvvmNavigationManager = mvvmNavigationManager;

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
    private async Task OpenTour(Tour tour) {
        _cache.CurrentTour = CloneTour(tour);
        _cache.CurrentTourLog = null;
        await Task.Yield();
        _mvvmNavigationManager.NavigateTo<ITourViewModel>();
    }

    private async Task LoadToursAsync() {
        var tours = string.IsNullOrWhiteSpace(TourSearchTerm)
            ? await _apiService.GetToursAsync()
            : await _apiService.SearchToursAsync(TourSearchTerm);

        Tours = tours.OrderBy(t => t.Name).ToList();
    }

    partial void OnTourSearchTermChanged(string value) {
        _ = UpdateToursAsync(value);
    }

    private async Task UpdateToursAsync(string searchTerm) {
        var normalizedSearchTerm = searchTerm.Trim();
        if (!string.Equals(TourSearchTerm, normalizedSearchTerm, StringComparison.Ordinal)) {
            TourSearchTerm = normalizedSearchTerm;
            return;
        }

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
        Logs = source.Logs.Select(log => new TourLog {
            Id = log.Id,
            Time = log.Time,
            Comment = log.Comment,
            Difficulty = log.Difficulty,
            TotalDistance = log.TotalDistance,
            TotalTime = log.TotalTime,
            Rating = log.Rating,
            TourId = log.TourId
        }).ToList()
    };
    
    [RelayCommand]
    private void CreateTour() {
        _cache.CurrentTour = null;
        _cache.CurrentTourLog = null;
        _mvvmNavigationManager.NavigateTo<ITourViewModel>();
    }
}