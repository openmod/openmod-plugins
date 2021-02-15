using Microsoft.AspNetCore.Components;
using OpenMod.Plugins.Extensions;

namespace OpenMod.Plugins.Services.Navigation
{
    public class QueryParser : IQueryParser
    {
        private readonly NavigationManager _navigationManager;

        public QueryParser(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        public void Search(string? uri, out int page, out string query)
        {
            uri ??= _navigationManager.Uri;
            page = _navigationManager.GetQuery<int>(uri, "page");
            query = _navigationManager.GetQuery<string>(uri, "query", defaultValue: "");
        }
    }
}
