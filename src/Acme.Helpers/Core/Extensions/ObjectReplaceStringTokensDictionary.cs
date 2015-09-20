using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace Acme.Helpers.Core.Extensions
{
    /// <exclude/>
    internal static partial class ExtensionMethods
    {
        /// <exclude/>
        public static string ReplaceStringTokens(this string @this, IDictionary<string, object> dictionary)
        {
            return @this
                .Replace(@"{{(\w+)}}", dictionary)
                .Replace(@"{(\w+)}", dictionary);
        }

        /// <exclude/>
        private static string Replace(this string @this, string mask, IDictionary<string, object> replacements)
        {
            return Regex.Replace(WebUtility.UrlDecode(@this), mask, (m) =>
            {
                object replacement;
                var key = m.Groups[1].Value;
                if (replacements.TryGetValue(key, out replacement))
                    return Convert.ToString(replacement);
                else
                    return m.Groups[0].Value;
            });
        }
    }
}
