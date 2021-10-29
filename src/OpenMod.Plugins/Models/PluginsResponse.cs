namespace OpenMod.Plugins.Models;

public record PluginsResponse(long Total, IReadOnlyList<Plugin> Plugins)
{
}
