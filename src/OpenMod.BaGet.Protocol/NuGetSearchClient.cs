using System.Net.Http.Json;
using System.Text;

namespace OpenMod.BaGet.Protocol;

public class NuGetSearchClient : ISearchClient
{
    private readonly HttpClient _httpClient;
    private readonly string _searchUrl;

    public NuGetSearchClient(HttpClient httpClient, string searchUrl)
    {
        _httpClient = httpClient;
        _searchUrl = searchUrl;
    }

    public async Task<SearchResponse> SearchAsync(
        string? query = null,
        int skip = 0,
        int take = 20,
        bool includePrerelease = true,
        bool includeSemVer2 = true,
        CancellationToken cancellationToken = default)
    {
        var requestUri = AddSearchQueryString(_searchUrl, query, skip, take, includePrerelease, includeSemVer2, "q");

        return (await _httpClient.GetFromJsonAsync<SearchResponse>(requestUri, cancellationToken))!;
    }

    private static string AddSearchQueryString(string uri, string? query, int? skip, int? take, bool includePrerelease, bool includeSemVer2, string queryParamName)
    {
        var dictionary = new Dictionary<string, string>();

        if (skip.HasValue && skip.Value > 0)
        {
            dictionary["skip"] = skip.ToString()!;
        }

        if (take.HasValue)
        {
            dictionary["take"] = take.ToString()!;
        }

        if (includePrerelease)
        {
            dictionary["prerelease"] = true.ToString();
        }

        if (includeSemVer2)
        {
            dictionary["semVerLevel"] = "2.0.0";
        }

        if (!string.IsNullOrEmpty(query))
        {
            dictionary[queryParamName] = query;
        }

        return AddQueryString(uri, dictionary);
    }

    private static string AddQueryString(string uri, Dictionary<string, string> queryString)
    {
        if (uri.IndexOf('#') != -1)
        {
            throw new InvalidOperationException("URL anchors are not supported");
        }

        if (uri.IndexOf('?') != -1)
        {
            throw new InvalidOperationException("Adding query strings to URL with query strings is not supported");
        }

        var stringBuilder = new StringBuilder(uri);
        var flag = false;

        foreach (var item in queryString)
        {
            stringBuilder.Append(flag ? '&' : '?');
            stringBuilder.Append(Uri.EscapeDataString(item.Key));
            stringBuilder.Append('=');
            stringBuilder.Append(Uri.EscapeDataString(item.Value));
            flag = true;
        }

        return stringBuilder.ToString();
    }
}
