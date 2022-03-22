using MudBlazor;

namespace Net6BlazorPwaLab.Models;

/// <summary>
/// Theme Resource
/// </summary>
internal static class CustomThemeResource
{
  public static readonly MudTheme DefaultTheme = new MudTheme()
  {
    Typography = new Typography(),
    Palette = new Palette
    {
      Primary = "#1daaa8ff",
      Secondary = "#ffc6a3ff",
      Tertiary = "#e2231aff",
      Info = "#2196f3ff",
      Success = "#00c853ff",
      Warning = "#ff9800ff",
      Error = "#f44336ff",
      Dark = "#585858ff",
      Surface = "#fafafaff",
      DrawerBackground = "#1daaa8ff",
      AppbarBackground = "#1daaa8ff",
      AppbarText = "rgba(255,255,255, 0.70)",
    },
  };

  public static readonly MudTheme DarkTheme = new MudTheme()
  {
    Typography = new Typography(),
    Palette = new Palette()
    {
      Black = "#27272f",
      Background = "#32333d",
      BackgroundGrey = "#27272f",
      Surface = "#373740",
      DrawerBackground = "#27272f",
      DrawerText = "rgba(255,255,255, 0.50)",
      DrawerIcon = "rgba(255,255,255, 0.50)",
      AppbarBackground = "#27272f",
      AppbarText = "rgba(255,255,255, 0.70)",
      TextPrimary = "rgba(255,255,255, 0.70)",
      TextSecondary = "rgba(255,255,255, 0.50)",
      ActionDefault = "#adadb1",
      ActionDisabled = "rgba(255,255,255, 0.26)",
      ActionDisabledBackground = "rgba(255,255,255, 0.12)",
      Divider = "rgba(255,255,255, 0.12)",
      DividerLight = "rgba(255,255,255, 0.06)",
      TableLines = "rgba(255,255,255, 0.12)",
      LinesDefault = "rgba(255,255,255, 0.12)",
      LinesInputs = "rgba(255,255,255, 0.3)",
      TextDisabled = "rgba(255,255,255, 0.2)"
    }
  };
}
