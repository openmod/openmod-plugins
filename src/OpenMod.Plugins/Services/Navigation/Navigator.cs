using Microsoft.AspNetCore.Components;

namespace OpenMod.Plugins.Services.Navigation;

public class Navigator : INavigator
{
    private readonly NavigationManager _navigationManager;
    private readonly IQueryParser _queryParser;
    private readonly IUriBuilder _uriBuilder;

    public Navigator(NavigationManager navigationManager, IQueryParser queryParser, IUriBuilder uriBuilder)
    {
        _navigationManager = navigationManager;
        _queryParser = queryParser;
        _uriBuilder = uriBuilder;
    }

    public void Index()
    {
        string uri = _uriBuilder.Index();
        _navigationManager.NavigateTo(uri);
    }

    public void Search(int? page, string? query)
    {
        int currentPage = 0;
        string currentQuery = "";

        if (page == null || query == null)
        {
            _queryParser.Search(null, out currentPage, out currentQuery);
        }

        string uri = _uriBuilder.Search(
            page ?? currentPage,
            query ?? currentQuery);

        _navigationManager.NavigateTo(uri);
    }
}
