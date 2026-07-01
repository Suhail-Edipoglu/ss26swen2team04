using Blazing.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SWEN2TourPlanner.Frontend.DTOs;
using SWEN2TourPlanner.Frontend.DTOs.Enums;

namespace SWEN2TourPlanner.Frontend.ViewModels.Interfaces;

public interface ITourViewModel : IViewModelBase {
    Tour TourData { get; set; }
    List<TourLog> TourLogs { get; set;  }
    TourViewMode CurrentView { get; set; }
    string TourLogSearchTerm { get; set; }
    Alert? SaveAlert { get; }
    IRelayCommand EnterEditModeCommand { get; }
    IRelayCommand ToggleCompactViewCommand { get; }
    IAsyncRelayCommand UpdateSearchResultsCommand { get; }
    IRelayCommand<TourLog> OpenTourLogCommand { get; }
    IAsyncRelayCommand SaveChangesCommand { get; }
    IAsyncRelayCommand DeleteTourCommand { get; }
    IRelayCommand CancelEditCommand { get; }
    IRelayCommand CreateTourLogCommand { get; }
    IRelayCommand CloseAlertCommand { get; }
}