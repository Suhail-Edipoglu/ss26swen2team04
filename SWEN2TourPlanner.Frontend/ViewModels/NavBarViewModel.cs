using Blazing.Mvvm.ComponentModel;
using Blazing.Mvvm.Components;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;
using MudBlazor;
using SWEN2TourPlanner.Frontend.DTOs;
using SWEN2TourPlanner.Frontend.DTOs.Enums;
using SWEN2TourPlanner.Frontend.DTOs.Messages;
using SWEN2TourPlanner.Frontend.Models.Interfaces;
using SWEN2TourPlanner.Frontend.ViewModels.Interfaces;

namespace SWEN2TourPlanner.Frontend.ViewModels;

[ViewModelDefinition<INavBarViewModel>]
public sealed partial class NavBarViewModel : RecipientViewModelBase, INavBarViewModel {
    private readonly IMvvmNavigationManager _mvvmNavigationManager;
    private  readonly ILoginManager _loginManager;
    public List<BreadcrumbItem> AppNavBarItems { get; set; }
    private readonly ICache _cache;
    private readonly ILogger _logger;

    public NavBarViewModel(IMvvmNavigationManager mvvmNavigationManager, ICache cache, ILoginManager loginManager, ILoggerFactory loggerFactory)
    {
        IsActive = true; // activates Message Receive
        _mvvmNavigationManager = mvvmNavigationManager;
        _loginManager = loginManager;
        AppNavBarItems = [new BreadcrumbItem("Loading", null, true)];
        _cache = cache;
        _logger = loggerFactory.CreateLogger("NavBarViewModel");
    }

    [RelayCommand]
    private void Logout() {
        _loginManager.Logout();
        _mvvmNavigationManager.NavigateTo<ILoginViewModel>();
    }

    [RelayCommand]
    private void ChangeTheme() {
        WeakReferenceMessenger.Default.Send(new ThemeChangedMessage());
    }

    public void Receive(NavbarStateChangedMessage message) {
        AppNavBarItems = message.NewState switch {
            NavbarState.Main => [
                new BreadcrumbItem("Tours", _mvvmNavigationManager.GetUri<IHomeViewModel>(), true)
            ],
            NavbarState.Tour => [
                new BreadcrumbItem("Tours", _mvvmNavigationManager.GetUri<IHomeViewModel>()),
                new BreadcrumbItem(_cache.CurrentTour?.Name ?? "New Tour", _mvvmNavigationManager.GetUri<ITourViewModel>(), true)
            ],
            NavbarState.TourLog => [
                new BreadcrumbItem("Home", _mvvmNavigationManager.GetUri<IHomeViewModel>()),
                new BreadcrumbItem(_cache.CurrentTour?.Name ?? "Tour", _mvvmNavigationManager.GetUri<ITourViewModel>()),
                new BreadcrumbItem(_cache.CurrentTourLog?.Time.ToString("dd.MM.yyyy") ?? "Tour Log",
                    _mvvmNavigationManager.GetUri<ITourLogViewModel>(), true)
            ],
            _ => AppNavBarItems
        };
        OnPropertyChanged(nameof(AppNavBarItems));

    }
    
}
