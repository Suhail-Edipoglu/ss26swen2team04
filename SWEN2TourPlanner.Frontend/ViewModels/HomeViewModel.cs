using Blazing.Mvvm.ComponentModel;
using SWEN2TourPlanner.Frontend.Services.Interfaces;
using SWEN2TourPlanner.Frontend.ViewModels.Interfaces;

namespace SWEN2TourPlanner.Frontend.ViewModels;

[ViewModelDefinition<IHomeViewModel>]
public sealed partial class HomeViewModel : ViewModelBase, IHomeViewModel {
    
}