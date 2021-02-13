using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BaGet.Protocol;
using OpenMod.Plugins.Data;

namespace OpenMod.Plugins.Services
{
    public class PluginRepository : IPluginRepository
    {
        private readonly NuGetClient _nuGetClient;

        public PluginRepository()
        {
            var httpClient = new HttpClient(new HttpClientHandler());
            var clientFactory = new NuGetClientFactory(httpClient, "https://api.nuget.org/v3/index.json");
            _nuGetClient = new NuGetClient(clientFactory);
        }

        public async Task<IReadOnlyList<Plugin>> SearchAsync(string query, int skip, int take, bool includePrerelease)
        {
            query += " Tags:\"openmod-plugin\"";
            return (await _nuGetClient.SearchAsync(query, skip, take, includePrerelease))
                .Select(x => new Plugin(x))
                .ToList();
        }
    }
}
