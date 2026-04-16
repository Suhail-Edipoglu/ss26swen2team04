using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SWEN2TourPlanner.Frontend.ViewModels.Interfaces;

namespace SWEN2TourPlanner.Frontend.ViewModels;

public partial class NavMenuViewModel : ObservableObject, INavMenuViewModel
{
    [ObservableProperty] private string? currentTour = "CurrentTour";
    [ObservableProperty] private string? currentTourLog;
    [ObservableProperty] private bool isMenuExpanded = true;
    
    [RelayCommand]
    private void ToggleDetails()
        => IsMenuExpanded = !IsMenuExpanded;
}