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
        var queries = new Dictionary<string, string>(capacity: 2);

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

    public string Plugin(string id)
    {
        var queries = new Dictionary<string, string>(capacity: 1);

        queries["id"] = id;

        return QueryHelpers.AddQueryString(uri: "plugin", queries);
    }
}
