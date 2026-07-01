using Blazing.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using MudBlazor;
using MudBlazor.Utilities;
using SWEN2TourPlanner.Frontend.DTOs.Messages;
using SWEN2TourPlanner.Frontend.ViewModels.Interfaces;

namespace SWEN2TourPlanner.Frontend.ViewModels;

[ViewModelDefinition<IThemeProviderViewModel>]
public partial class ThemeProvider(ILoggerFactory loggerFactory) : RecipientViewModelBase, IThemeProviderViewModel {
    ILogger _logger =  loggerFactory.CreateLogger("ThemeProvider");
    [ObservableProperty]
    private MudTheme _theme = MudThemeGenerator(new MudTheme());

    public void Receive(ThemeChangedMessage message) {
        _logger.LogInformation("Theme Changed");
        // TODO Themes
    }

    private static MudTheme MudThemeGenerator(MudTheme input) {
        input.PaletteLight.TextDisabled = input.PaletteLight.TextPrimary;
        input.PaletteDark.TextDisabled = input.PaletteDark.TextPrimary;
        return input;
    }
}