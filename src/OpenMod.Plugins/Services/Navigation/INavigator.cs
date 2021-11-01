namespace OpenMod.Plugins.Services.Navigation;

public interface INavigator
{
    void Index();

    void Search(int? page, string? query);

    void Plugin(string id);
}
