using System.Collections.Generic;
using System.Linq;
using BaGet.Protocol.Models;

namespace OpenMod.Plugins.Data
{
    public record Plugin(string Id, string Title, string Description, IReadOnlyList<string> Authors)
    {
        public Plugin(SearchResult searchResult) : this(
            searchResult.PackageId,
            searchResult.Title,
            searchResult.Description,
            searchResult.Authors
                .SelectMany(x => x.Split(',').Select(y => y.Trim()))
                .ToList())
        {
        }

        public bool IsOfficial => Authors.Any(x => x.Trim().ToLower() == "openmod");

        public string CommandInstall => "openmod install " + Id;
    }
}
