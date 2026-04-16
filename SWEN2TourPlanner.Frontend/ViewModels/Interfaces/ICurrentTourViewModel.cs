using System.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SWEN2TourPlanner.Models;

namespace SWEN2TourPlanner.Frontend.ViewModels.Interfaces;

public interface ICurrentTourViewModel : INotifyPropertyChanged {
    bool EditMode { get; set; }
    Tour CurrentTour { get; set; }
    IRelayCommand SaveCommand { get; }
    Tour LoadTourById(int id);
}
