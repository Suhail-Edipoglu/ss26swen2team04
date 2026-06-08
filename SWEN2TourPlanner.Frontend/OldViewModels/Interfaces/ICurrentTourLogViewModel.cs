using System.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SWEN2TourPlanner.Frontend.DTOs;

namespace SWEN2TourPlanner.Frontend.ViewModels.Interfaces;

public interface ICurrentTourLogViewModel : INotifyPropertyChanged{
    TourLog CurrentTourLog { get; set; }
    IRelayCommand SaveCommand { get; }
    TourLog LoadTourLogById(int id);
    void Delete(int id);
}
