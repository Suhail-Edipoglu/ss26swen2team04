using Blazing.Mvvm.ComponentModel;
using Blazing.Mvvm.Components;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MudBlazor;
using SWEN2TourPlanner.Frontend.DTOs;
using SWEN2TourPlanner.Frontend.DTOs.Enums;
using SWEN2TourPlanner.Frontend.DTOs.Messages;
using SWEN2TourPlanner.Frontend.Models.Interfaces;
using SWEN2TourPlanner.Frontend.Services.Interfaces;
using SWEN2TourPlanner.Frontend.ViewModels.Interfaces;

namespace SWEN2TourPlanner.Frontend.ViewModels;

[ViewModelDefinition<ITourViewModel>]
public sealed partial class TourViewModel(IApiService apiService, ICache cache, IMvvmNavigationManager mvvmNavigationManager, ILoggerFactory loggerFactory) : ViewModelBase, ITourViewModel {
    private readonly IApiService _apiService = apiService;
    private readonly ICache _cache = cache;
    private readonly IMvvmNavigationManager _mvvmNavigationManager = mvvmNavigationManager;
    private readonly ILogger _logger = loggerFactory.CreateLogger("TourViewModel");

    [ObservableProperty]
    private Tour _tourData = new() {
        Name = string.Empty,
        Description = string.Empty,
        From = string.Empty,
        To = string.Empty,
        TransportType = TransportType.None,
        Distance = 0,
        EstimatedTime = new TimeSpan(0, 0, 0),
        RouteInformation = string.Empty,
        UserId = 0
    };
    
    [ObservableProperty]
    private List<TourLog> _tourLogs = [];

    [ObservableProperty]
    private TourViewMode _currentView = TourViewMode.Full;

    [ObservableProperty]
    private string _tourLogSearchTerm = string.Empty;

    [ObservableProperty]
    private Alert? _saveAlert;

    private Tour? _originalTourData;

    public override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        if (_cache.CurrentTour is null) {
            TourData = CreateEmptyTour();
            CurrentView = TourViewMode.Create;
            _logger.LogInformation("Create Mode");
        }
        else {
            TourData = CloneTour(_cache.CurrentTour);
            CurrentView = TourViewMode.Full;
            _logger.LogInformation("View Mode");
            await LoadTourLogsAsync();
        }

        SaveAlert = null;
        WeakReferenceMessenger.Default.Send(new NavbarStateChangedMessage(NavbarState.Tour));
    }

    [RelayCommand]
    private void EnterEditMode() {
        _originalTourData = CloneTour(TourData);
        SaveAlert = null;
        CurrentView = TourViewMode.Edit;
        _logger.LogInformation("Edit Mode");
    }

    [RelayCommand]
    private void ToggleCompactView() {
        if (CurrentView == TourViewMode.Edit) {
            return;
        } else if (CurrentView == TourViewMode.Compact) {
            CurrentView = TourViewMode.Full;
            _logger.LogInformation("View Mode");
        } else if (CurrentView == TourViewMode.Full) {
            CurrentView = TourViewMode.Compact;
            _logger.LogInformation("Compact Mode");
        }
        
    }

    [RelayCommand]
    private async Task UpdateSearchResults() {
        await LoadTourLogsAsync();
    }
    
    [RelayCommand]
    private void OpenTourLog(TourLog tourLog) {
        _cache.CurrentTourLog = CloneTourLog(tourLog);
        _cache.CurrentTour = CloneTour(TourData);
        _logger.LogInformation("Opening Tour Log");
        _mvvmNavigationManager.NavigateTo<ITourLogViewModel>();
    }

    [RelayCommand]
    private async Task SaveChanges() {
        RecalculateFrontendValues();

        if (CurrentView == TourViewMode.Create) {
            var newTourId = await _apiService.CreateTourAsync(TourData);
            if (newTourId <= 0) {
                SaveAlert = new Alert("Creating tour failed.", Severity.Error);
                _logger.LogInformation("Creating Tour Failed");
                return;
            }

            Tour createdTour;
            try {
                createdTour = await _apiService.GetTourByIdAsync(newTourId);
            }
            catch {
                createdTour = CloneTour(TourData);
                createdTour.Id = newTourId;
            }

            TourData = CloneTour(createdTour);
            await LoadTourLogsAsync();
            _cache.CurrentTour = CloneTour(createdTour);
            SaveAlert = new Alert("Tour created successfully.", Severity.Success);
            _logger.LogInformation("Tour created Successfully");
            CurrentView = TourViewMode.Full;
            _logger.LogInformation("View Mode");
            _originalTourData = null;
            _mvvmNavigationManager.NavigateTo<ITourViewModel>();
            return;
        }

        var updateSuccessful = await _apiService.UpdateTourAsync(TourData);
        if (!updateSuccessful) {
            SaveAlert = new Alert("Saving tour failed.", Severity.Error);
            return;
        }

        _cache.CurrentTour = CloneTour(TourData);
        SaveAlert = new Alert("Tour saved successfully.", Severity.Success);
        CurrentView = TourViewMode.Full;
        _logger.LogInformation("View Mode");
        _originalTourData = null;
        await LoadTourLogsAsync();
    }

    [RelayCommand]
    private async Task DeleteTour() {
        if (CurrentView == TourViewMode.Create) {
            return;
        }

        if (TourData.Id is null) {
            SaveAlert = new Alert("Delete failed: tour has no id.", Severity.Error);
            return;
        }

        var deleteSuccessful = await _apiService.DeleteTourAsync(TourData.Id.Value);
        if (!deleteSuccessful) {
            SaveAlert = new Alert("Deleting tour failed.", Severity.Error);
            _logger.LogWarning("Deleting Tour Failed");
            return;
        }

        SaveAlert = new Alert("Tour deleted successfully.", Severity.Success);
        _logger.LogInformation("Tour deleted");
        CurrentView = TourViewMode.Full;
        _originalTourData = null;
        _cache.CurrentTour = null;
        _mvvmNavigationManager.NavigateTo<IHomeViewModel>();
    }

    [RelayCommand]
    private void CancelEdit() {
        if (CurrentView == TourViewMode.Create) {
            _mvvmNavigationManager.NavigateTo<IHomeViewModel>();
            return;
        }

        if (_originalTourData is not null) {
            TourData = CloneTour(_originalTourData);
        }

        CurrentView = TourViewMode.Full;
        _originalTourData = null;
    }

    private void RecalculateFrontendValues() {
        // TODO
        TourData.Distance = null;
        TourData.EstimatedTime = null;
    }

    [RelayCommand]
    private void CreateTourLog() {
        _cache.CurrentTourLog = null;
        _logger.LogInformation("Creating Tour Log");
        _mvvmNavigationManager.NavigateTo<ITourLogViewModel>();
    }

    private async Task LoadTourLogsAsync() {
        if (TourData.Id is null) {
            TourLogs = [];
            return;
        }

        var logs = string.IsNullOrWhiteSpace(TourLogSearchTerm)
            ? await _apiService.GetTourLogsAsync(TourData.Id.Value)
            : await _apiService.SearchTourLogsAsync(TourData.Id.Value, TourLogSearchTerm);

        TourLogs = logs.OrderByDescending(l => l.Time).ToList();
        _logger.LogInformation("Loaded Tour Logs");
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

    private static TourLog CloneTourLog(TourLog source) => new() {
        Id = source.Id,
        Time = source.Time,
        Comment = source.Comment,
        Difficulty = source.Difficulty,
        TotalDistance = source.TotalDistance,
        TotalTime = source.TotalTime,
        Rating = source.Rating,
        TourId = source.TourId
    };

    private static Tour CreateEmptyTour() => new() {
        Name = string.Empty,
        Description = string.Empty,
        From = string.Empty,
        To = string.Empty,
        TransportType = TransportType.None,
        Distance = null,
        EstimatedTime = null,
        RouteInformation = string.Empty,
        UserId = 0,
        Popularity = null,
        ChildFriendliness = null,
    };

    [RelayCommand]
    private void CloseAlert() {
        SaveAlert = null;
    }
}