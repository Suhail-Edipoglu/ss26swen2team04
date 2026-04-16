using Blazing.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SWEN2TourPlanner.Frontend.ViewModels.Interfaces;

namespace SWEN2TourPlanner.Frontend.ViewModels;

[ViewModelDefinition(Lifetime = ServiceLifetime.Singleton)]
public sealed partial class NavMenuViewModel : ViewModelBase, INavMenuViewModel
{
    [ObservableProperty] private string _currentTour = "CurrentTourName";
    [ObservableProperty] private string _currentTourLog = "SelectedTourLogName";
    [ObservableProperty] private bool _isMenuExpanded = true;
    [ObservableProperty] private int? _currentTourId;
    
        
    [RelayCommand]
    private void ToggleDetails()
        => IsMenuExpanded = !IsMenuExpanded;

    public void UpdateCurrentTourId(int? id)
    {
        CurrentTourId = id;
    } 
}
