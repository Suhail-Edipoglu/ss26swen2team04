using Blazing.Mvvm.ComponentModel;
using Blazing.Mvvm.Components;
using SWEN2TourPlanner.Frontend.Models.Interfaces;
using SWEN2TourPlanner.Frontend.ViewModels.Interfaces;

namespace SWEN2TourPlanner.Frontend.ViewModels;

[ViewModelDefinition<ILoginCheckerViewModel>]
public class LoginCheckerViewModel(IMvvmNavigationManager navigationManager, ILoginManager loginManager) : ViewModelBase, ILoginCheckerViewModel {
    private readonly ILoginManager _loginManager = loginManager;
    private readonly IMvvmNavigationManager _navigationManager = navigationManager;

    public override void OnParametersSet() {
        if (!_loginManager.IsLoggedIn()) {
            // _navigationManager.NavigateTo<ILoginViewModel>();
        }
        base.OnParametersSet();
    }
}
