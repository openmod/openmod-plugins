using Microsoft.AspNetCore.WebUtilities;

namespace OpenMod.Plugins.Services.Navigation;

public class UriBuilder : IUriBuilder
{
    public string Index()
    {
        return "";
    }

    public string Search(int page, string query)
    {
        var queries = new Dictionary<string, string>(capacity: 5);

        if (page != 0)
        {
            queries["page"] = page.ToString();
        }

        if (query != "")
        {
            queries["query"] = query;
        }

        return QueryHelpers.AddQueryString(uri: "search", queries);
    }
}
