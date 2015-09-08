using System;
using System.Text.RegularExpressions;
using Microsoft.AspNet.Mvc.ModelBinding;
using System.Text;
using System.Net;

namespace Acme.Helpers.Core.Extensions
{
    /// <exclude/>
    internal static partial class ExtensionMethods
    {
        /// <exclude/>
        public static string ReplaceStringTokens(this ModelExplorer explorer, string src)
            => ReplaceStringTokens(src, explorer);

        /// <exclude/>
        public static string ReplaceStringTokens(this object src, ModelExplorer explorer)
            => ReplaceStringTokens(src.ToString(), explorer);

        /// <exclude/>
        public static string ReplaceStringTokens(this string src, ModelExplorer explorer)
        {
            return src.Replace(@"{{(\w+)}}", explorer).Replace(@"{(\w+)}", explorer);
        }

        /// <exclude/>
        private static string Replace(this string src, string mask, ModelExplorer explorer)
        {
            return Regex.Replace(WebUtility.UrlDecode(src), mask, (m) =>
            {
                var key = m.Groups[1].Value;
                ModelExplorer property = explorer.GetExplorerForProperty(key);
                return (property?.Model?.ToString() ?? m.Groups[0].Value);
            });
        }
    }
}
