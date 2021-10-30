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

    private static readonly string[] _ownerBlacklist =
    {
        // Trademark violations
        "FPlugins",
    };

    public PluginRepository(IClientFactory clientFactory)
    {
        _searchClient = clientFactory.GetSearchClient();
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
            .Where(x => !IsBlacklisted(x)
                && x.Tags != null
                && x.Tags.Contains("openmod-plugin", StringComparer.OrdinalIgnoreCase))
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

        if (plugin.Owners == null)
        {
            return true;
        }

        if (_ownerBlacklist
            .Any(b => plugin.Owners.Any(x => x.Equals(b, StringComparison.OrdinalIgnoreCase))))
        {
            return true;
        }

        return false;
    }
}
