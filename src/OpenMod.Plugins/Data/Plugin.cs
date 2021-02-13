using System;
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

        public bool IsOfficial => Id.StartsWith("OpenMod.", StringComparison.OrdinalIgnoreCase)
                                  && Authors.Any(x => x.Trim().Equals("openmod", StringComparison.OrdinalIgnoreCase));

        public string CommandInstall => "openmod install " + Id;
    }
}
