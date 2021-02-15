using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using MudBlazor.Services;
using OpenMod.Plugins.Services;
using OpenMod.Plugins.Services.Navigation;

namespace OpenMod.Plugins
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(
                sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});

            builder.Services.AddSingleton<INavigator, Navigator>();
            builder.Services.AddSingleton<IQueryParser, QueryParser>();
            builder.Services.AddSingleton<IUriBuilder, OpenMod.Plugins.Services.Navigation.UriBuilder>();
            builder.Services.AddSingleton<IPluginRepository, PluginRepository>();

            builder.Services.AddMudBlazorSnackbar(config =>
            {
                config.PositionClass = Defaults.Classes.Position.TopRight;
                config.PreventDuplicates = false;
                config.NewestOnTop = false;
                config.ShowCloseIcon = true;
                config.VisibleStateDuration = 5000;
                config.HideTransitionDuration = 500;
                config.ShowTransitionDuration = 300;
                config.SnackbarVariant = Variant.Filled;
                config.MaxDisplayedSnackbars = 7;
            });
            builder.Services.AddMudServices();

            await builder.Build().RunAsync();
        }
    }
}
