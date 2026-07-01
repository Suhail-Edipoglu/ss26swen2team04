using Blazing.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using MudBlazor;
using MudBlazor.Utilities;
using SWEN2TourPlanner.Frontend.DTOs.Messages;
using SWEN2TourPlanner.Frontend.ViewModels.Interfaces;

namespace SWEN2TourPlanner.Frontend.ViewModels;

[ViewModelDefinition<IThemeProviderViewModel>]
public partial class ThemeProvider(ILoggerFactory loggerFactory) : RecipientViewModelBase, IThemeProviderViewModel {
    private ILogger _logger =  loggerFactory.CreateLogger("ThemeProvider");
    [ObservableProperty]
    private MudTheme _theme = MudThemeGenerator(new MudTheme());
    
    public void Receive(ThemeChangedMessage message) {
        _logger.LogInformation("Theme Changed");
        SwapColours(Theme);
        DarkLightModeToggle(Theme);
        OnPropertyChanged(nameof(Theme));
    }

    private static MudTheme MudThemeGenerator(MudTheme input) {
        input.PaletteLight.TextDisabled = input.PaletteLight.TextPrimary;
        input.PaletteDark.TextDisabled = input.PaletteDark.TextPrimary;
        return input;
    }

    private static void DarkLightModeToggle(MudTheme input) {
        (input.PaletteLight, input.PaletteDark) = (input.PaletteDark, input.PaletteLight);
    }

    private static void SwapColours(MudTheme input) {
        (MudColor LightColour, MudColor DarkColour)[] colourPairs = [
            (new MudColor(30, 0.72, 0.50, 255), new MudColor(30, 0.72, 0.70, 255)),
            (new MudColor(102, 0.72, 0.50, 255), new MudColor(102, 0.72, 0.70, 255)),
            (new MudColor(174, 0.72, 0.50, 255), new MudColor(174, 0.72, 0.70, 255)),
            (new MudColor(246, 0.72, 0.50, 255), new MudColor(246, 0.72, 0.70, 255)),
            (new MudColor(318, 0.72, 0.50, 255), new MudColor(318, 0.72, 0.70, 255)),
        ];

        MudColor lightColour = new MudColor(245, 0.72, 0.58, 1);
        MudColor darkColour = new MudColor(245, 0.72, 0.66, 1);
        var selectedPair = colourPairs[Random.Shared.Next(colourPairs.Length)];

        input.PaletteLight.Primary = selectedPair.LightColour;
        input.PaletteDark.Primary = selectedPair.DarkColour;
    }
}