using System.Windows.Input;
using Blazing.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SWEN2TourPlanner.Frontend.DTOs;

namespace SWEN2TourPlanner.Frontend.ViewModels.Interfaces;

public interface ILoginViewModel : IViewModelBase {
    UserData LoginData { get; set; }
    UserData RegisterData { get; set; }
    IAsyncRelayCommand LoginCommand { get; }
    IAsyncRelayCommand RegisterCommand { get; }
    Alert? LoginAlert { get; }
    Alert? RegistrationAlert { get; }
}
