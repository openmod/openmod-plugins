using System.Linq;

namespace OpenMod.Plugins.Data
{
    public record PluginCardData(string Id, string Summary, string[] Owners)
    {
        public bool IsOfficial => Owners.Any(x => x.Trim().ToLower() == "openmod");

        public string CommandInstall => "openmod install " + Id;
    }
}
