using Blazing.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SWEN2TourPlanner.Frontend.Services;
using SWEN2TourPlanner.Frontend.ViewModels.Interfaces;
using SWEN2TourPlanner.Models;

namespace SWEN2TourPlanner.Frontend.ViewModels;

[ViewModelDefinition(Lifetime = ServiceLifetime.Singleton)]
public sealed partial class CurrentTourLogViewModel : ViewModelBase, ICurrentTourLogViewModel {
    
[ObservableProperty] private Log _currentTourLog = new();

    private readonly ILogService _logService;

    public CurrentTourLogViewModel(ILogService logService)
    {
        _logService = logService;
    }

    [RelayCommand]
    private void Save()
    {
        if (CurrentTourLog.Id is null)
        {
            _logService.CreateLog(CurrentTourLog);
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
        }
        return CurrentTourLog;
    }

    public void Delete(int id)
    {
        _logService.DeleteLog(id);
    }
}
