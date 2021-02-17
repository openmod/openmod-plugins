using System;
using System.Net.Http;
using System.Threading.Tasks;
using BaGet.Protocol;
using OpenMod.Plugins.Models;

namespace OpenMod.Plugins.Services
{
    public class PluginRepository : IPluginRepository
    {
        private readonly ISearchClient _searchClient;

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
            var response = await _searchClient.SearchAsync(query, skip, take, includePrerelease);
            return new PluginsResponse(response);
        }
    }
}
