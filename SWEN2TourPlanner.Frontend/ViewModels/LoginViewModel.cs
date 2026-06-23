using Blazing.Mvvm.ComponentModel;
using Blazing.Mvvm.Components;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SWEN2TourPlanner.Frontend.DTOs;
using SWEN2TourPlanner.Frontend.Models.Interfaces;
using SWEN2TourPlanner.Frontend.ViewModels.Interfaces;

namespace SWEN2TourPlanner.Frontend.ViewModels;

[ViewModelDefinition<ILoginViewModel>]
public sealed partial class LoginViewModel(ILoginManager loginManager, IMvvmNavigationManager mvvmNavigationManager) : ViewModelBase, ILoginViewModel {
    private readonly ILoginManager _loginManager = loginManager;
    private readonly IMvvmNavigationManager _mvvmNavigationManager = mvvmNavigationManager;
    [ObservableProperty]
    private UserData _loginData = new();
    [ObservableProperty]
    private UserData _registerData = new();

    [RelayCommand]
    private void Login() {
        if (_loginManager.Login(LoginData))
        {
            _mvvmNavigationManager.NavigateTo<IHomeViewModel>();
        }
        else {
            // NOTIFY Login failed
        }
    }
    [RelayCommand]
    private void Register() {
        if (_loginManager.Register(RegisterData)) {
            // NOTIFY Register success
        }
        else {
            // NOTIFY Register failed
        }
    }
}