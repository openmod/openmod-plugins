using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OpenMod.Plugins.Shared;

namespace OpenMod.Plugins;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");

        var clientFactory = await CreateNuGetClientFactoryAsync();

        builder.Services.AddSingleton<IClientFactory, NuGetClientFactory>(services => clientFactory);

        builder.Services.AddScoped(
            sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

        builder.Services.AddSingleton<INavigator, Navigator>();
        builder.Services.AddSingleton<IQueryParser, QueryParser>();
        builder.Services.AddSingleton<IUriBuilder, OpenMod.Plugins.Services.Navigation.UriBuilder>();
        builder.Services.AddSingleton<IPluginRepository, PluginRepository>();

        builder.Services.AddScoped<PageTitle.PageTitleState>();

        await builder.Build().RunAsync();
    }

    private static async Task<NuGetClientFactory> CreateNuGetClientFactoryAsync()
    {
        var httpClient = new HttpClient(new HttpClientHandler());
        var serviceIndexUrl = "https://api.nuget.org/v3/index.json";

        return await NuGetClientFactory.CreateAsync(httpClient, serviceIndexUrl);
    }
}
