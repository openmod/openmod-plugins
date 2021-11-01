using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OpenMod.Plugins.Shared;

namespace OpenMod.Plugins;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddTransient(
            sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

        var clientFactory = await CreateNuGetClientFactoryAsync();

        builder.Services.AddSingleton<IClientFactory, NuGetClientFactory>(services => clientFactory);
        builder.Services.AddSingleton<INavigator, Navigator>();
        builder.Services.AddSingleton<IQueryParser, QueryParser>();
        builder.Services.AddSingleton<IUriBuilder, OpenMod.Plugins.Services.Navigation.UriBuilder>();
        builder.Services.AddSingleton<IPluginRepository, PluginRepository>();
        builder.Services.AddSingleton<IMarkdownService, MarkdownService>();
        builder.Services.AddSingleton<IClipboardService, ClipboardService>();

        builder.Services.AddScoped<OpenMod.Plugins.Shared.PageTitle.PageTitleState>();

        await builder.Build().RunAsync();
    }

    private static async Task<NuGetClientFactory> CreateNuGetClientFactoryAsync()
    {
        var httpClient = new HttpClient(new HttpClientHandler());
        var serviceIndexUrl = "https://api.nuget.org/v3/index.json";

        return await NuGetClientFactory.CreateAsync(httpClient, serviceIndexUrl);
    }
}
