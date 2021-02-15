using System.Collections.Generic;
using Microsoft.AspNetCore.WebUtilities;

namespace OpenMod.Plugins.Services.Navigation
{
    public class UriBuilder : IUriBuilder
    {
        public string Index()
        {
            return "";
        }

        public string Search(int page, string query)
        {
            var @params = new Dictionary<string, string>
            {
                {"page", page.ToString()},
                {"query", query},
            };
            return QueryHelpers.AddQueryString(uri: "search", @params);
        }
    }
}
