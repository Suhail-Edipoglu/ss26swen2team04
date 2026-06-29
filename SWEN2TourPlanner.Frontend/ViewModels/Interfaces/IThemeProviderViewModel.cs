using Blazing.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using MudBlazor;
using SWEN2TourPlanner.Frontend.DTOs.Messages;

namespace SWEN2TourPlanner.Frontend.ViewModels.Interfaces;

public interface IThemeProviderViewModel : IViewModelBase, IRecipient<ThemeChangedMessage> {
    MudTheme Theme { get; set; }
}