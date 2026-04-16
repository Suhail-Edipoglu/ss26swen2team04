using Microsoft.AspNetCore.Components;
using SWEN2TourPlanner.Frontend.Services;
using SWEN2TourPlanner.Frontend.ViewModels.Interfaces;
using SWEN2TourPlanner.Models;

namespace SWEN2TourPlanner.Frontend.ViewModels;

public class CurrentTourViewModel : ICurrentTourViewModel {
    public bool EditMode { get; set; }
    public Tour? CurrentTour { get; set; }
    [Inject]
    public ITourService? TourService { get; set; }
}