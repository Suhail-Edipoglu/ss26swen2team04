using Blazing.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Components;
using SWEN2TourPlanner.Frontend.ViewModels.Interfaces;

namespace SWEN2TourPlanner.Frontend.ViewModels;

[ViewModelDefinition<INavMenuViewModel>(Lifetime = ServiceLifetime.Scoped)]
public sealed partial class NavMenuViewModel : ViewModelBase, INavMenuViewModel
{
    [ObservableProperty] private string _currentTour = "CurrentTourName";
    [ObservableProperty] private string _currentTourLog = "SelectedTourLogName";
    [ObservableProperty] private bool _isMenuExpanded = true;
    private int? _currentTourId = null;

    private readonly NavigationManager _navigationManager;
    public NavMenuViewModel(NavigationManager navigationManager) {
        _navigationManager = navigationManager;
    }
    
    [RelayCommand]
    private void ToggleDetails()
        => IsMenuExpanded = !IsMenuExpanded;

    public void RootClicked() {
        _navigationManager.NavigateTo("/");
    }
    public void TourClicked() {
        if (_currentTourId != null) {
            _navigationManager.NavigateTo("/tour?" + _currentTourId);
        }
    }
}
