using Blazing.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using SWEN2TourPlanner.Frontend.Services;
using SWEN2TourPlanner.Frontend.Services.Interfaces;
using SWEN2TourPlanner.Frontend.ViewModels.Interfaces;
using SWEN2TourPlanner.Models;

namespace SWEN2TourPlanner.Frontend.ViewModels;

[ViewModelDefinition(Lifetime = ServiceLifetime.Scoped)]
public sealed partial class TourListViewModel : ViewModelBase, ITourListViewModel
{
    [ObservableProperty] private List<Tour> _tours;

    private readonly ITourService _tourService;
    
    public TourListViewModel(ITourService tourService)
    {
        _tourService = tourService;
    }
    public void LoadTours()
    {
        _tours = _tourService.GetTours(0) ?? new List<Tour>();
    }

}