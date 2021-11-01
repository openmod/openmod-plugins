namespace OpenMod.Plugins.Services;

public interface IPluginRepository
{
    Task<PluginsResponse> SearchAsync(string query, int skip, int take, CancellationToken cancellationToken = default);

    Task<Plugin?> GetPluginAsync(string id, CancellationToken cancellationToken = default);

    Task<string> GetMarkdownAsync(Plugin plugin, CancellationToken cancellationToken = default);
}
