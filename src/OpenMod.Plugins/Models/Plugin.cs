using System;
using System.Collections.Generic;
using System.Linq;
using BaGet.Protocol.Models;

namespace OpenMod.Plugins.Models
{
    public record Plugin(
        string Id,
        string Title,
        string Description,
        IReadOnlyList<string> Authors,
        string? SiteUrl)
    {
        public Plugin(SearchResult searchResult) : this(
            searchResult.PackageId,
            searchResult.Title,
            searchResult.Description,
            searchResult.Authors
                .SelectMany(x => x.Split(',').Select(y => y.Trim()))
                .ToList(),
            searchResult.ProjectUrl)
        {
        }

        public bool IsOfficial => Id.StartsWith("OpenMod.", StringComparison.OrdinalIgnoreCase)
                                  && Authors.Any(x => x == Id || x.Trim().Equals("openmod", StringComparison.OrdinalIgnoreCase));

        public string CommandInstall => "openmod install " + Id;

        public string NuGetUrl => "https://www.nuget.org/packages/" + Id;
    }
}
