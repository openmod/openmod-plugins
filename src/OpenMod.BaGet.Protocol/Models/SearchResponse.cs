namespace OpenMod.BaGet.Protocol.Models;

// See https://docs.microsoft.com/en-us/nuget/api/search-query-service-resource#response
public class SearchResponse
{
    [JsonPropertyName("totalHits")]
    public long TotalHits { get; set; }

    [JsonPropertyName("data")]
    public IReadOnlyList<SearchResult> Data { get; set; } = null!;
}
