using Blazing.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SWEN2TourPlanner.Frontend.DTOs;

namespace SWEN2TourPlanner.Frontend.ViewModels.Interfaces;

public interface IHomeViewModel : IViewModelBase {
    List<Tour> Tours { get; set; }
    string TourSearchTerm { get; set; }
    IAsyncRelayCommand<Tour> OpenTourCommand { get; }
    IRelayCommand CreateTourCommand { get; }
}