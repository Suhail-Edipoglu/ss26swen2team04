using Blazing.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SWEN2TourPlanner.Frontend.ViewModels.Interfaces;

namespace SWEN2TourPlanner.Frontend.ViewModels;

[ViewModelDefinition(Lifetime = ServiceLifetime.Singleton)]
public sealed partial class NavMenuViewModel : ViewModelBase, INavMenuViewModel
{
    [ObservableProperty] private string currentTour = "CurrentTourName";
    [ObservableProperty] private string currentTourLog = "SelectedTourLogName";
    [ObservableProperty] private bool isMenuExpanded = true;
        
    [RelayCommand]
    private void ToggleDetails()
        => IsMenuExpanded = !IsMenuExpanded;
    
}
