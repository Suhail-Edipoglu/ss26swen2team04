using Blazing.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
namespace SWEN2TourPlanner.Frontend.ViewModels.Interfaces;

public interface INavMenuViewModel : IViewModelBase {
    string CurrentTour { get; set; }
    string CurrentTourLog { get; set; }
    bool IsMenuExpanded { get; set; }
    void RootClicked();
    void TourClicked();
    IRelayCommand ToggleDetailsCommand { get; }
}
