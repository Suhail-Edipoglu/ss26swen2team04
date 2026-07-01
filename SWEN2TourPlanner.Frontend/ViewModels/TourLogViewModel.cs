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


[ViewModelDefinition<ITourLogViewModel>]
public sealed partial class TourLogViewModel(IApiService apiService, ICache cache, IMvvmNavigationManager mvvmNavigationManager, ILoggerFactory loggerFactory) : ViewModelBase, ITourLogViewModel {
    private readonly IApiService _apiService = apiService;
    private readonly ICache _cache = cache;
    private readonly IMvvmNavigationManager _mvvmNavigationManager = mvvmNavigationManager;
    private readonly ILogger _logger = loggerFactory.CreateLogger("TourLogViewModel");

    [ObservableProperty]
    private TourLog _tourLogData = CreateEmptyTourLog();

    [ObservableProperty]
    private TourLogViewMode _currentView = TourLogViewMode.Full;

    [ObservableProperty]
    private Alert? _saveAlert;

    private TourLog? _originalTourLogData;

    public TimeSpan? SelectedTotalTime {
        get => TourLogData?.TotalTime;
        set {
            if (TourLogData != null) {
                TourLogData.TotalTime = value ?? TimeSpan.Zero;
            }
        }
    }

    public override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        if (_cache.CurrentTourLog is null) {
            TourLogData = CreateEmptyTourLog();
            if (_cache.CurrentTour?.Id is not null) {
                TourLogData.TourId = _cache.CurrentTour.Id.Value;
            }
            else {
                _logger.LogWarning("No Tour Selected, Navigating to Home");
                _mvvmNavigationManager.NavigateTo<IHomeViewModel>();
            }
            CurrentView = TourLogViewMode.Create;
            _logger.LogInformation("Create Mode");
            _logger.LogInformation("No Tour Log Selected, Creating New Tour Log");

        }
        else {
            TourLogData = CloneTourLog(_cache.CurrentTourLog);
            CurrentView = TourLogViewMode.Full;
            _logger.LogInformation("View Mode");
            _logger.LogInformation("Tour Log Loaded");

        }

        SaveAlert = null;
        WeakReferenceMessenger.Default.Send(new NavbarStateChangedMessage(NavbarState.TourLog));
    }

    [RelayCommand]
    private void EnterEditMode() {
        _originalTourLogData = CloneTourLog(TourLogData);
        SaveAlert = null;
        CurrentView = TourLogViewMode.Edit;
        _logger.LogInformation("Edit Mode");
    }

    [RelayCommand]
    private async Task SaveChanges() {
        if (!TryApplyTourContext()) {
            SaveAlert = new Alert("No tour is selected for this log.", Severity.Error);
            _logger.LogError("No tour is selected for this log.");
            return;
        }

        if (CurrentView == TourLogViewMode.Create) {
            var newLogId = await _apiService.CreateTourLogAsync(TourLogData);
            if (newLogId <= 0) {
                SaveAlert = new Alert("Creating tour log failed.", Severity.Error);
                _logger.LogWarning("Creating Tour Log Failed");
                return;
            }

            TourLog createdTourLog;
            try {
                createdTourLog = await _apiService.GetTourLogByIdAsync(newLogId);
            }
            catch {
                createdTourLog = CloneTourLog(TourLogData);
                createdTourLog.Id = newLogId;
            }

            TourLogData = CloneTourLog(createdTourLog);
            _cache.CurrentTourLog = CloneTourLog(createdTourLog);
            SaveAlert = new Alert("Tour log created successfully.", Severity.Success);
            CurrentView = TourLogViewMode.Full;
            _logger.LogInformation("View Mode");
            _originalTourLogData = null;
            _logger.LogInformation("Tour Log Created");
            _mvvmNavigationManager.NavigateTo<ITourLogViewModel>();
            return;
        }

        var updateSuccessful = await _apiService.UpdateTourLogAsync(TourLogData);
        if (!updateSuccessful) {
            SaveAlert = new Alert("Saving tour log failed.", Severity.Error);
            _logger.LogWarning("Saving tour log failed.");

            return;
        }

        _cache.CurrentTourLog = CloneTourLog(TourLogData);
        SaveAlert = new Alert("Tour log saved successfully.", Severity.Success);
        CurrentView = TourLogViewMode.Full;
        _logger.LogInformation("View Mode");
        _originalTourLogData = null;
        _logger.LogInformation("Tour Log Updated");
    }

    [RelayCommand]
    private async Task DeleteTourLog() {
        if (CurrentView == TourLogViewMode.Create) {
            return;
        }

        if (TourLogData.Id is null) {
            SaveAlert = new Alert("Delete failed: tour log has no id.", Severity.Error);
            _logger.LogError("Delete failed: tour log has no id.");
            return;
        }

        var deleteSuccessful = await _apiService.DeleteTourLogAsync(TourLogData.Id.Value);
        if (!deleteSuccessful) {
            SaveAlert = new Alert("Deleting tour log failed.", Severity.Error);
            _logger.LogWarning("Deleting tour log failed.");
            return;
        }

        SaveAlert = new Alert("Tour log deleted successfully.", Severity.Success);
        _cache.CurrentTourLog = null;
        _originalTourLogData = null;
        _logger.LogInformation("Tour Log deleted");


        if (_cache.CurrentTour is not null) {
            _mvvmNavigationManager.NavigateTo<ITourViewModel>();
        }
        else {
            _mvvmNavigationManager.NavigateTo<IHomeViewModel>();
        }
    }

    [RelayCommand]
    private void CancelEdit() {
        if (CurrentView == TourLogViewMode.Create) {
            if (_cache.CurrentTour is not null) {
                _mvvmNavigationManager.NavigateTo<ITourViewModel>();
            }
            else {
                _mvvmNavigationManager.NavigateTo<IHomeViewModel>();
            }

            return;
        }

        if (_originalTourLogData is not null) {
            TourLogData = CloneTourLog(_originalTourLogData);
        }

        CurrentView = TourLogViewMode.Full;
        _logger.LogInformation("View Mode");
        _originalTourLogData = null;
    }
    
    private bool TryApplyTourContext() {
        if (_cache.CurrentTour?.Id is null) {
            return TourLogData.TourId != 0;
        }

        TourLogData.TourId = _cache.CurrentTour.Id.Value;
        return true;
    }

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

    private static TourLog CreateEmptyTourLog() => new() {
        Id = null,
        Time = DateTimeOffset.Now,
        Comment = string.Empty,
        Difficulty = 1,
        TotalDistance = 0,
        TotalTime = new TimeSpan(0, 0, 0),
        Rating = 1,
        TourId = 0
    };

    [RelayCommand]
    private void CloseAlert() {
        SaveAlert = null;
    }
}