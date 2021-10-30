using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OpenMod.Plugins.Shared;

namespace OpenMod.Plugins;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");

        builder.Services.AddScoped(
            sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

        builder.Services.AddSingleton<INavigator, Navigator>();
        builder.Services.AddSingleton<IQueryParser, QueryParser>();
        builder.Services.AddSingleton<IUriBuilder, OpenMod.Plugins.Services.Navigation.UriBuilder>();
        builder.Services.AddSingleton<IPluginRepository, PluginRepository>();

        builder.Services.AddScoped<PageTitle.PageTitleState>();

        await builder.Build().RunAsync();
    }
}
