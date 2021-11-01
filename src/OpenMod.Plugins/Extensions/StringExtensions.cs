namespace OpenMod.Plugins.Extensions;

public static class StringExtensions
{
    public static string TrimStart(this string target, string trimString)
    {
        if (string.IsNullOrEmpty(trimString))
        {
            return target;
        }

        var result = target;

        while (result.StartsWith(trimString))
        {
            result = result[trimString.Length..];
        }

        return result;
    }
}
