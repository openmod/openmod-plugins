namespace OpenMod.BaGet.Protocol.Models;

// See https://docs.microsoft.com/en-us/nuget/api/service-index#resources
public class ServiceIndexItem
{
    [JsonPropertyName("@id")]
    public string ResourceUrl { get; set; } = null!;

    [JsonPropertyName("@type")]
    public string Type { get; set; } = null!;

    [JsonPropertyName("comment")]
    public string? Comment { get; set; }
}
