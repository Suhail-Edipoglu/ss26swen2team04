namespace SWEN2TourPlanner.Frontend.ViewModels.Interfaces;

public interface ICurrentTourViewModel {
    bool EditMode { get; set; }
    String Distance { get; set; }
    String Time { get; set; }
}