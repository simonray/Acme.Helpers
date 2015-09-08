using System;

namespace Acme.Helpers.Core.Extensions
{
    /// <exclude/>
    internal static partial class ExtensionMethods
    {
        /// <exclude/>
        public static string ToLowerString(this object value)
            => value?.ToString().ToLower();
    }
}
