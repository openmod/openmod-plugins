namespace OpenMod.Plugins.Models;

public record Plugin(
    string Id,
    string Description,
    IReadOnlyList<string> Owners,
    string? SiteUrl,
    long? TotalDownloads,
    string LatestVersion,
    IReadOnlyList<string> Tags,
    string? LicenseUrl)
{
    public Plugin(SearchResult searchResult) : this(
        searchResult.PackageId,
        FixDescription(searchResult.Description ?? ""),
        searchResult.Owners ?? Array.Empty<string>(),
        searchResult.ProjectUrl,
        searchResult.TotalDownloads,
        searchResult.Version,
        searchResult.Tags ?? Array.Empty<string>(),
        searchResult.LicenseUrl)
    {
    }

    public bool IsOfficial { get; }
        = Id.StartsWith("OpenMod.", StringComparison.OrdinalIgnoreCase)
          && Owners.Any(x => x == Id || x.Equals("OpenMod", StringComparison.OrdinalIgnoreCase));

    public string CommandInstall { get; } = "openmod install " + Id;

    public string NuGetUrl { get; } = "https://www.nuget.org/packages/" + Id;

    private static string FixDescription(string description)
    {
        return description.Trim() == "Package Description" ? "" : description;
    }
}
