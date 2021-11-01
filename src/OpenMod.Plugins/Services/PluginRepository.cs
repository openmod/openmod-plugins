namespace OpenMod.Plugins.Services;

public class PluginRepository : IPluginRepository
{
    private readonly Cache<PluginsResponse> _searchCache;
    private readonly ISearchClient _searchClient;
    private readonly HttpClient _httpClient;

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

    public PluginRepository(IClientFactory clientFactory, HttpClient httpClient)
    {
        _searchCache = new Cache<PluginsResponse>(TimeSpan.FromMinutes(5), SearchAsyncInternal);
        _searchClient = clientFactory.GetSearchClient();
        _httpClient = httpClient;
    }

    public async Task<PluginsResponse> SearchAsync(string query, int skip, int take, CancellationToken cancellationToken = default)
    {
        var value = await _searchCache.GetValueAsync(cancellationToken);

        query = query.Trim();

        var pluginsFiltered = value.Plugins
            .Skip(skip)
            .Take(take);

        if (!string.IsNullOrWhiteSpace(query))
        {
            pluginsFiltered = pluginsFiltered.Where(p => p.Id.Contains(query, StringComparison.OrdinalIgnoreCase));
        }

        return new PluginsResponse(value.Total, pluginsFiltered.ToList());
    }

    private async Task<PluginsResponse> SearchAsyncInternal(CancellationToken cancellationToken = default)
    {
        var response = await _searchClient.SearchAsync("Tags:\"openmod-plugin\"", 0, 1000, cancellationToken: cancellationToken);

        Filter(response, out var plugins, out var total);

        return new PluginsResponse(total, plugins);
    }

    public async Task<Plugin?> GetPluginAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await _searchCache.GetValueAsync(cancellationToken);

        return response.Plugins.FirstOrDefault(p => p.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<string?> GetMarkdownAsync(Plugin plugin, CancellationToken cancellationToken = default)
    {
        // https://github.com/NuGet/NuGetGallery/issues/8732#issuecomment-892953185
        var uri = @$"https://api.nuget.org/v3-flatcontainer/{plugin.Id.ToLowerInvariant()}/{plugin.Version}/readme";

        var responce = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, uri), cancellationToken);

        if (!responce.IsSuccessStatusCode)
        {
            return null;
        }

        return await responce.Content.ReadAsStringAsync(cancellationToken);
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

        if (_ownerBlacklist.Any(b => plugin.Owners.Any(x => x.Equals(b, StringComparison.OrdinalIgnoreCase))))
        {
            return true;
        }

        return false;
    }
}
