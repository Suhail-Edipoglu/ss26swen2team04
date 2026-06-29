using Blazing.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using MudBlazor;
using MudBlazor.Utilities;
using SWEN2TourPlanner.Frontend.DTOs.Messages;
using SWEN2TourPlanner.Frontend.ViewModels.Interfaces;

namespace SWEN2TourPlanner.Frontend.ViewModels;

[ViewModelDefinition<IThemeProviderViewModel>]
public partial class ThemeProvider : RecipientViewModelBase, IThemeProviderViewModel {
    
    [ObservableProperty]
    private MudTheme _theme = new MudTheme() {
        PaletteLight = new PaletteLight() {
            TextDisabled = "rgba(66,66,66,1)"
        },
        PaletteDark = new PaletteDark() {
            TextDisabled = "rgba(255,255,255,0.6980392156862745)"
        }
    };

    public void Receive(ThemeChangedMessage message) {
        Console.WriteLine($"Theme Changed");
    }

}