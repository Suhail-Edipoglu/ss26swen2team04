using SWEN2TourPlanner.Frontend.Services;
using SWEN2TourPlanner.Models;

namespace SWEN2TourPlanner.Frontend.ViewModels.Interfaces;

public interface ICurrentTourViewModel {
    bool EditMode { get; set; }
    Tour? CurrentTour { get; set; }
    public ITourService? TourService { get; set; }
}
