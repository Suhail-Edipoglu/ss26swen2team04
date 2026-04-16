using CommunityToolkit.Mvvm.Input;
namespace SWEN2TourPlanner.Frontend.ViewModels.Interfaces;

public interface INavMenuViewModel {
    string? CurrentTour { get; set; }
    string? CurrentTourLog { get; set; }
    bool IsMenuExpanded { get; set; }
    IRelayCommand ToggleDetailsCommand { get; }
}
