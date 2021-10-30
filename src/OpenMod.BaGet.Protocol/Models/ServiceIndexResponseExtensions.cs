namespace OpenMod.BaGet.Protocol.Models;

public static class ServiceIndexResponseExtensions
{
    private static readonly string _version340 = "/3.4.0";
    private static readonly string _version300beta = "/3.0.0-beta";

    private static readonly string[] _searchQueryService = new[]
    {
        "SearchQueryService" + _version340,
        "SearchQueryService" + _version300beta,
        "SearchQueryService"
    };

    public static string GetSearchQueryResourceUrl(this ServiceIndexResponse serviceIndex)
    {
        return serviceIndex.GetRequiredResourceUrl(_searchQueryService, "SearchQueryService");
    }

    public static string? GetResourceUrl(this ServiceIndexResponse serviceIndex, string[] types)
    {
        return types
            .SelectMany(t => serviceIndex.Resources.Where(r => r.Type == t))
            .FirstOrDefault()?
            .ResourceUrl
            .Trim('/');
    }

    public static string GetRequiredResourceUrl(this ServiceIndexResponse serviceIndex, string[] types, string resourceName)
    {
        var resourceUrl = serviceIndex.GetResourceUrl(types);

        if (string.IsNullOrEmpty(resourceUrl))
        {
            throw new InvalidOperationException("The service index does not have a resource named '" + resourceName + "'");
        }

        return resourceUrl;
    }
}
