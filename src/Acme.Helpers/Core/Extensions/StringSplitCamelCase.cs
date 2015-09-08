using System.Text.RegularExpressions;

namespace Acme.Helpers.Core.Extensions
{
    /// <exclude/>
    internal static partial class ExtensionMethods
    {
        /// <exclude/>
        public static string SplitCamelCase(this string str, char sep = ' ')
            => Regex.Replace(Regex.Replace(str, @"(\P{Ll})(\P{Ll}\p{Ll})", $"$1{sep}$2"), @"(\p{Ll})(\P{Ll})", $"$1{sep}$2");
        
        /// <exclude/>
        public static string SplitCamelCaseLower(this string str, char sep = ' ')
            => SplitCamelCase(str, sep).ToLower();
        
        /// <exclude/>
        public static string SplitCamelCaseLowerDash(this string str)
            => SplitCamelCase(str, '-').ToLower();
    }
}
