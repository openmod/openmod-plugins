using System.Net.Http.Json;

namespace OpenMod.BaGet.Protocol;

public class NuGetServiceIndexClient : IServiceIndexClient
{
    private readonly HttpClient _httpClient;
    private readonly string _serviceIndexUrl;

    public NuGetServiceIndexClient(HttpClient httpClient, string serviceIndexUrl)
    {
        _httpClient = httpClient;
        _serviceIndexUrl = serviceIndexUrl;
    }

    public async Task<ServiceIndexResponse> GetAsync(CancellationToken cancellationToken = default)
    {
        return (await _httpClient.GetFromJsonAsync<ServiceIndexResponse>(_serviceIndexUrl, cancellationToken))!;
    }
}
