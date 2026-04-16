using Blazing.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Components;
using SWEN2TourPlanner.Frontend.Services;
using SWEN2TourPlanner.Frontend.ViewModels.Interfaces;
using SWEN2TourPlanner.Models;

namespace SWEN2TourPlanner.Frontend.ViewModels;

[ViewModelDefinition(Lifetime = ServiceLifetime.Scoped)]
public sealed partial class CurrentTourLogViewModel : ViewModelBase, ICurrentTourLogViewModel {
    
[ObservableProperty] private Log _currentTourLog = new();

    private readonly ILogService _logService;
    private readonly NavigationManager _navigationManager;
    private readonly INavMenuViewModel _navMenuViewModel;

    public CurrentTourLogViewModel(
        ILogService logService,
        NavigationManager navigationManager,
        INavMenuViewModel navMenuViewModel)
    {
        _logService = logService;
        _navigationManager = navigationManager;
        _navMenuViewModel = navMenuViewModel;
    }

    [RelayCommand]
    private void Save()
    {
        if (CurrentTourLog.Id is null)
        {
            int? id = _logService.CreateLog(CurrentTourLog);
            if (id is not null)
            {
                _navigationManager.NavigateTo("/log?id=" + id);
            }
        }
        else
        {
            _logService.UpdateLog(CurrentTourLog);
        }
    }

    public Log LoadTourLogById(int id)
    {
        var loadedLog = _logService.GetLogById(id);
        if (loadedLog is not null)
        {
            CurrentTourLog = loadedLog;
            _navMenuViewModel.CurrentTourId = CurrentTourLog.TourId;
        }
        else
        {
            _navigationManager.NavigateTo("/");
        }
        return CurrentTourLog;

    }

    public void Delete(int id)
    {
        _logService.DeleteLog(id);
        _navigationManager.NavigateTo("/tour?id=" + CurrentTourLog.TourId);
    }
}
