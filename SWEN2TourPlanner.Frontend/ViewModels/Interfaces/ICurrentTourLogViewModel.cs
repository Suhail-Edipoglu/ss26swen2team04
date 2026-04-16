using System.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SWEN2TourPlanner.Models;

namespace SWEN2TourPlanner.Frontend.ViewModels.Interfaces;

public interface ICurrentTourLogViewModel : INotifyPropertyChanged{
    Log CurrentTourLog { get; set; }
    IRelayCommand SaveCommand { get; }
    Log LoadTourLogById(int id);
    void Delete(int id);
}
