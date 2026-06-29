using Blazing.Mvvm.ComponentModel;
using Blazing.Mvvm.Components;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MudBlazor;
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
    [ObservableProperty]
    private Alert? _loginAlert = null;
    [ObservableProperty]
    private Alert? _registrationAlert = null;

    [RelayCommand]
    private async Task LoginAsync() {
        if (await _loginManager.LoginAsync(LoginData))
        {
            _mvvmNavigationManager.NavigateTo<IHomeViewModel>();
        }
        else {
            LoginAlert = new Alert("Login Failed", Severity.Error);
        }
    }
    [RelayCommand]
    private async Task RegisterAsync() {
        if (await _loginManager.RegisterAsync(RegisterData)) {
            RegistrationAlert = new Alert("Registration Successful", Severity.Success);
        }
        else {
            RegistrationAlert = new Alert("Registration Failed", Severity.Error);
        }
    }
}
