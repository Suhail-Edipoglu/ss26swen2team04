using Blazing.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Components;
using SWEN2TourPlanner.Frontend.Services;
using SWEN2TourPlanner.Frontend.Services.Interfaces;
using SWEN2TourPlanner.Frontend.ViewModels.Interfaces;
using SWEN2TourPlanner.Models;

namespace SWEN2TourPlanner.Frontend.ViewModels;

[ViewModelDefinition(Lifetime = ServiceLifetime.Scoped)]
public sealed partial class CurrentTourViewModel : ViewModelBase, ICurrentTourViewModel
{
    [ObservableProperty] private Tour _currentTour = new();

    private readonly ITourService _tourService;
    private readonly NavigationManager _navigationManager;

    public CurrentTourViewModel(ITourService tourService, NavigationManager navigationManager)
    {
        _tourService = tourService;
        _navigationManager = navigationManager;
    }

    [RelayCommand]
    private void Save()
    {
        if (CurrentTour.Id is null)
        {
            int? id = _tourService.CreateTour(CurrentTour);
            if (id is not null)
            {
                _navigationManager.NavigateTo("/tour?id=" + id);
            }
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
        else
        {
            _navigationManager.NavigateTo("/");
        }
        return CurrentTour;
        
    }

    public void Delete(int id)
    {
        _tourService.DeleteTour(id);
        _navigationManager.NavigateTo("/");

    }
}
