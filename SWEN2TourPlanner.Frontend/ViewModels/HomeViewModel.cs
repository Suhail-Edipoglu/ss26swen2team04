using Blazing.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using MudBlazor;
using SWEN2TourPlanner.Frontend.DTOs;
using SWEN2TourPlanner.Frontend.DTOs.Messages;
using SWEN2TourPlanner.Frontend.Services.Interfaces;
using SWEN2TourPlanner.Frontend.ViewModels.Interfaces;

namespace SWEN2TourPlanner.Frontend.ViewModels;

[ViewModelDefinition<IHomeViewModel>]
public sealed partial class HomeViewModel : ViewModelBase, IHomeViewModel {
    public override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        WeakReferenceMessenger.Default.Send(new NavbarStateChangedMessage(NavbarState.Main));
    }
}