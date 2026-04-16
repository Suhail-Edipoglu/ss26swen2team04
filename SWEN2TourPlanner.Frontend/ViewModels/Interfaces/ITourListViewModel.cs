using System.ComponentModel;
using SWEN2TourPlanner.Models;

namespace SWEN2TourPlanner.Frontend.ViewModels.Interfaces;

public interface ITourListViewModel : INotifyPropertyChanged
{
    List<Tour> Tours { get; set; }
    void LoadTours();
}