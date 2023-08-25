namespace OpenMod.Plugins.Models;

public record Plugin(
    string Id,
    string Description,
    string Summary,
    IReadOnlyList<string> Owners,
    string? ProjectUrl,
    long? TotalDownloads,
    string Version,
    IReadOnlyList<string> Tags,
    string? LicenseUrl)
{
    public Plugin(SearchResult searchResult) : this(
        searchResult.PackageId,
        FixDescription(searchResult.Description ?? ""),
        FixDescription(searchResult.Summary ?? ""),
        searchResult.Owners ?? Array.Empty<string>(),
        searchResult.ProjectUrl,
        searchResult.TotalDownloads,
        searchResult.Version,
        searchResult.Tags ?? Array.Empty<string>(),
        searchResult.LicenseUrl)
    {
    }

    public bool IsOfficial { get; } =
        Id.StartsWith("OpenMod.", StringComparison.OrdinalIgnoreCase)
        && Owners.Any(x => x == Id || x.Equals("OpenMod", StringComparison.OrdinalIgnoreCase));

    public string InstallCommand { get; } = $"om install {Id}@{Version}";

    public string NuGetPage { get; } = $"https://nuget.org/packages/{Id}/{Version}";

    private static string FixDescription(string description)
    {
        var trimmedDescription = description
            .AsSpan()
            .Trim();

        return trimmedDescription.SequenceEqual("Package Description") ||
               trimmedDescription.SequenceEqual("Your plugin description")
            ? ""
            : description;
    }
}
