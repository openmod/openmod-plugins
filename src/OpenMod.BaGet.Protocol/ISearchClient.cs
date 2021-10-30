namespace OpenMod.BaGet.Protocol;

// See https://docs.microsoft.com/en-us/nuget/api/search-query-service-resource
public interface ISearchClient
{
    Task<SearchResponse> SearchAsync(string? query = null,
        int skip = 0,
        int take = 20,
        bool includePrerelease = true,
        bool includeSemVer2 = true,
        CancellationToken cancellationToken = default);
}
