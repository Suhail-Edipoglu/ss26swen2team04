using SWEN2TourPlanner.Frontend.ViewModels.Interfaces;

namespace SWEN2TourPlanner.Frontend.ViewModels;

public class CurrentTourViewModel : ICurrentTourViewModel {
    public bool EditMode { get; set; }
    public string Distance { get; set; } = "3km";
    public string Time { get; set; } = "3h20";
}