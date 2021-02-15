using System;
using System.ComponentModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace OpenMod.Plugins.Extensions
{
    public static class NavigationManagerExtensions
    {
        public static T? GetQuery<T>(this NavigationManager navigationManager, string uri, string key)
        {
            var result = GetQuery(navigationManager, uri, typeof(T), key);
            return result == default ? default : (T) result;
        }

        public static T GetQuery<T>(this NavigationManager navigationManager, string uri, string key, T defaultValue)
        {
            var result = GetQuery(navigationManager, uri, typeof(T), key);
            return result == default ? defaultValue : (T) result;
        }

        public static object? GetQuery(this NavigationManager navigationManager, string uri, Type type, string key)
        {
            var absoluteUri = navigationManager.ToAbsoluteUri(uri);

            if (QueryHelpers.ParseQuery(absoluteUri.Query).TryGetValue(key, out var query))
            {
                var converter = TypeDescriptor.GetConverter(type);
                return converter.ConvertFrom((string) query);
            }

            return null;
        }
    }
}
