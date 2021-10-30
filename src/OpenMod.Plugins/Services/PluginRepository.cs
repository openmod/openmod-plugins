using BaGet.Protocol;
using BaGet.Protocol.Models;

namespace OpenMod.Plugins.Services;

public class PluginRepository : IPluginRepository
{
    private readonly ISearchClient _searchClient;

    private static readonly string[] _packageBlacklist =
    {
        // Trademark violations
        "VaultPlugin",
        "F.AntiCosmetics",
        "F.ItemRestrictions",

        // Test plugins
        "Bibble.Openmod.Test",
        "openmod_test",
        "TestPlugin9270",
        "SDRPTest",
        "MyName.MyUnturnedPlugin",
        "Iravi1.TestingPlugin",
        "TestingPlugin",
        "MyPlugin",
        "MyOpenModPlugin",
    };

    private static readonly string[] _authorBlacklist =
    {
        // Trademark violations
        "FPlugins",
    };

    public PluginRepository()
    {
        var httpClient = new HttpClient(new HttpClientHandler());
        var clientFactory = new NuGetClientFactory(httpClient, "https://api.nuget.org/v3/index.json");
        _searchClient = clientFactory.CreateSearchClient();
    }

    public async Task<PluginsResponse> SearchAsync(string query, int skip, int take, bool includePrerelease)
    {
        query = query.Replace("tags:", "", StringComparison.OrdinalIgnoreCase);
        query += " Tags:\"openmod-plugin\"";

        var response = await _searchClient.SearchAsync(query, 0, 1000, includePrerelease);

        Filter(response, out var plugins, out var total);

        var pluginsRange = plugins
            .Skip(skip)
            .Take(take)
            .ToList();

        return new PluginsResponse(total, pluginsRange);
    }

    private static void Filter(SearchResponse searchResponse, out IReadOnlyList<Plugin> plugins, out long total)
    {
        plugins = searchResponse.Data
            .Where(x => !IsBlacklisted(x) && x.Tags.Contains("openmod-plugin", StringComparer.OrdinalIgnoreCase))
            .Select(x => new Plugin(x))
            .ToList();

        total = searchResponse.TotalHits - (searchResponse.Data.Count - plugins.Count);
    }

    private static bool IsBlacklisted(SearchResult plugin)
    {
        if (_packageBlacklist.Any(b => plugin.PackageId.Trim().Equals(b, StringComparison.OrdinalIgnoreCase)))
        {
            return true;
        }

        if (_authorBlacklist.Any(b => plugin.Authors.Any(x => x.Contains(b, StringComparison.OrdinalIgnoreCase))))
        {
            return true;
        }

        return false;
    }
}
