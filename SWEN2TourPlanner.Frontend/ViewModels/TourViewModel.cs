using Blazing.Mvvm.ComponentModel;
using SWEN2TourPlanner.Frontend.ViewModels.Interfaces;

namespace SWEN2TourPlanner.Frontend.ViewModels;

[ViewModelDefinition<ITourViewModel>]
public sealed partial class TourViewModel : ViewModelBase, ITourViewModel {
    
}