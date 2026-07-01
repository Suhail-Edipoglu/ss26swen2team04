using Blazing.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SWEN2TourPlanner.Frontend.DTOs;
using SWEN2TourPlanner.Frontend.DTOs.Enums;

namespace SWEN2TourPlanner.Frontend.ViewModels.Interfaces;

public interface ITourLogViewModel : IViewModelBase {
    TourLog TourLogData { get; set; }
    TimeSpan? SelectedTotalTime { get; set; }
    TourLogViewMode CurrentView { get; set; }
    Alert? SaveAlert { get; }
    IRelayCommand EnterEditModeCommand { get; }
    IAsyncRelayCommand SaveChangesCommand { get; }
    IAsyncRelayCommand DeleteTourLogCommand { get; }
    IRelayCommand CancelEditCommand { get; }
    IRelayCommand CloseAlertCommand { get; }
}