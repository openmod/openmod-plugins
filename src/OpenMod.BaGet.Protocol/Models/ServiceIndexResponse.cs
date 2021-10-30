namespace OpenMod.BaGet.Protocol.Models;

public class ServiceIndexResponse
{
    [JsonPropertyName("version")]
    public string Version { get; set; } = null!;

    [JsonPropertyName("resources")]
    public IReadOnlyList<ServiceIndexItem> Resources { get; set; } = null!;
}
