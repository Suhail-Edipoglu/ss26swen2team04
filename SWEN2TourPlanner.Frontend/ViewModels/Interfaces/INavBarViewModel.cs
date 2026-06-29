using Blazing.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MudBlazor;
using SWEN2TourPlanner.Frontend.DTOs;
using SWEN2TourPlanner.Frontend.DTOs.Messages;

namespace SWEN2TourPlanner.Frontend.ViewModels.Interfaces;

public interface INavBarViewModel : IViewModelBase, IRecipient<NavbarStateChangedMessage> {
    List<BreadcrumbItem> AppNavBarItems { get; set; }
    IRelayCommand LogoutCommand { get; }
    IRelayCommand ChangeThemeCommand { get; }
}
