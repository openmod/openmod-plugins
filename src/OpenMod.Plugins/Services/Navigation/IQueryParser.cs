namespace OpenMod.Plugins.Services.Navigation;

public interface IQueryParser
{
    void Search(string? uri, out int page, out string query);
}
