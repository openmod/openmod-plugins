using System;
using System.Collections.Generic;
using System.Linq;
using BaGet.Protocol.Models;

namespace OpenMod.Plugins.Models
{
    public record PluginsResponse(long Total, IReadOnlyList<Plugin> Plugins)
    {
        public PluginsResponse(SearchResponse searchResponse) : this(
            searchResponse.TotalHits,
            GetValidPlugins(searchResponse))
        {
        }

        private static IReadOnlyList<Plugin> GetValidPlugins(SearchResponse searchResponse)
        {
            return searchResponse.Data
                .Where(x => x.Tags.Contains("openmod-plugin", StringComparer.OrdinalIgnoreCase))
                .Select(x => new Plugin(x))
                .ToList();
        }
    }
}
