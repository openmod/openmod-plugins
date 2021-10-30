namespace OpenMod.BaGet.Protocol;

public class NuGetClientFactory : IClientFactory
{
    private readonly HttpClient _httpClient;
    private readonly string _serviceIndexUrl;
    private ServiceIndexResponse? _serviceIndexResponce;

    private NuGetClientFactory(HttpClient httpClient, string serviceIndexUrl)
    {
        _httpClient = httpClient;
        _serviceIndexUrl = serviceIndexUrl;
    }

    public static async Task<NuGetClientFactory> CreateAsync(
        HttpClient httpClient,
        string serviceIndexUrl,
        CancellationToken cancellationToken = default)
    {
        var factory = new NuGetClientFactory(httpClient, serviceIndexUrl);
        var serviceIndexClient = factory.GetServiceIndexClient();

        factory._serviceIndexResponce = await serviceIndexClient.GetAsync(cancellationToken);

        return factory;
    }

    public IServiceIndexClient GetServiceIndexClient()
    {
        return new NuGetServiceIndexClient(_httpClient, _serviceIndexUrl);
    }

    public ISearchClient GetSearchClient()
    {
        var searchUrl = _serviceIndexResponce!.GetSearchQueryResourceUrl();
        return new NuGetSearchClient(_httpClient, searchUrl);
    }
}
