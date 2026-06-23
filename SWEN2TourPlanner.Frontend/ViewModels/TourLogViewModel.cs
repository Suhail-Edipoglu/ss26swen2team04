using Blazing.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using SWEN2TourPlanner.Frontend.DTOs;
using SWEN2TourPlanner.Frontend.DTOs.Messages;
using SWEN2TourPlanner.Frontend.ViewModels.Interfaces;

namespace SWEN2TourPlanner.Frontend.ViewModels;


[ViewModelDefinition<ITourLogViewModel>]
public sealed partial class TourLogViewModel : ViewModelBase, ITourLogViewModel {
    public override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        WeakReferenceMessenger.Default.Send(new NavbarStateChangedMessage(NavbarState.TourLog));
    }
}