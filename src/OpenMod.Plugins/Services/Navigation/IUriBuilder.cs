namespace OpenMod.Plugins.Services.Navigation;

public interface IUriBuilder
{
    string Index();

    string Search(int page, string query);
}
