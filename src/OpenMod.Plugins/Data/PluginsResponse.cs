using System.Collections.Generic;
using System.Linq;
using BaGet.Protocol.Models;

namespace OpenMod.Plugins.Data
{
    public record PluginsResponse(long Total, IReadOnlyList<Plugin> Plugins)
    {
        public PluginsResponse(SearchResponse searchResponse) : this(
            searchResponse.TotalHits,
            searchResponse.Data.Select(x => new Plugin(x)).ToList())
        {
        }
    }
}
