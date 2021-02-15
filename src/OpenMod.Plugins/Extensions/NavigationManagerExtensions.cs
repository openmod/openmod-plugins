using System;
using System.ComponentModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace OpenMod.Plugins.Extensions
{
    public static class NavigationManagerExtensions
    {
        public static T? GetQuery<T>(this NavigationManager navigationManager, string key)
        {
            var result = GetQuery(navigationManager, typeof(T), key);
            return result == default ? default : (T) result;
        }

        public static T GetQuery<T>(this NavigationManager navigationManager, string key, T defaultValue)
        {
            var result = GetQuery(navigationManager, typeof(T), key);
            return result == default ? defaultValue : (T) result;
        }

        public static object? GetQuery(this NavigationManager navigationManager, Type type, string key)
        {
            var uri = navigationManager.ToAbsoluteUri(navigationManager.Uri);

            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue(key, out var query))
            {
                var converter = TypeDescriptor.GetConverter(type);
                return converter.ConvertFrom((string) query);
            }

            return null;
        }
    }
}
