using System.Collections.Generic;
using MudBlazor;

namespace OpenMod.Plugins.Shared
{
    public partial class MainLayout
    {
        private MudTheme _currentTheme;

        public MainLayout()
        {
            Themes = new MudTheme[]
            {
                new(), // default
                new() // dark
                {
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
                        ActionDisabledBackground = "rgba(255,255,255, 0.12)"
                    },
                },
            };
            _currentTheme = Themes[0];
        }

        public IList<MudTheme> Themes { get; }

        public void RotateTheme()
        {
            int current = Themes.IndexOf(_currentTheme);
            int next = current == -1
                ? 0
                : current + 1 >= Themes.Count
                    ? 0
                    : current + 1;

            _currentTheme = Themes[next];
        }
    }
}
