using Blazing.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SWEN2TourPlanner.Frontend.Services;
using SWEN2TourPlanner.Frontend.ViewModels.Interfaces;
using SWEN2TourPlanner.Models;

namespace SWEN2TourPlanner.Frontend.ViewModels;

[ViewModelDefinition(Lifetime = ServiceLifetime.Singleton)]
public sealed partial class CurrentTourViewModel : ViewModelBase, ICurrentTourViewModel
{
    [ObservableProperty] private Tour _currentTour = new();

    private readonly ITourService _tourService;

    public CurrentTourViewModel(ITourService tourService)
    {
        _tourService = tourService;
    }

    [RelayCommand]
    private void Save()
    {
        if (CurrentTour.Id is null)
        {
            _tourService.CreateTour(CurrentTour);
        }
        else
        {
            _tourService.UpdateTour(CurrentTour);
        }
    }

    public Tour LoadTourById(int id)
    {
        var loadedTour = _tourService.GetTourById(id);
        if (loadedTour is not null)
        {
            CurrentTour = loadedTour; 
        }
        return CurrentTour;
    }

    public void Delete(int id)
    {
        _tourService.DeleteTour(id);
    }
}
