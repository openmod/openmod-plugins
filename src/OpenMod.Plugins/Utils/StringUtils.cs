using System.Collections.Generic;
using System.Linq;

namespace OpenMod.Plugins.Utils
{
    public static class StringUtils
    {
        public static string Commaize(IReadOnlyList<string> sequence)
        {
            return sequence.Count switch
            {
                0 => "",
                1 => sequence[0],
                _ => string.Join(", ", sequence.Take(sequence.Count - 1)) + " and " + sequence[^1]
            };
        }
    }
}
