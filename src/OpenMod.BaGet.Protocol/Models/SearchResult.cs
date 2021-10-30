namespace OpenMod.BaGet.Protocol.Models;

// See https://docs.microsoft.com/en-us/nuget/api/search-query-service-resource#search-result
public class SearchResult
{
    [JsonPropertyName("id")]
    public string PackageId { get; set; } = null!;

    [JsonPropertyName("version")]
    public string Version { get; set; } = null!;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("owners")]
    [JsonConverter(typeof(StringOrStringArrayJsonConverter))]
    public IReadOnlyList<string>? Owners { get; set; }

    [JsonPropertyName("iconUrl")]
    public string? IconUrl { get; set; }

    [JsonPropertyName("licenseUrl")]
    public string? LicenseUrl { get; set; }

    [JsonPropertyName("projectUrl")]
    public string? ProjectUrl { get; set; }

    [JsonPropertyName("summary")]
    public string? Summary { get; set; }

    [JsonPropertyName("tags")]
    public IReadOnlyList<string>? Tags { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("totalDownloads")]
    public long? TotalDownloads { get; set; }
}
