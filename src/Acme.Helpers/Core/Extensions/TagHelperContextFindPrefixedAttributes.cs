using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Acme.Helpers.Core.Extensions
{
    /// <exclude/>
    internal static partial class ExtensionMethods
    {
        /// <summary>
        /// Returns all attributes from <paramref name="tagHelperContext"/>'s.
        /// </summary>
        public static IDictionary<string, object> FindPrefixedAttributes(this TagHelperContext @this, string prefix)
            => @this.AllAttributes
                .Where(attribute => attribute.Name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                .ToDictionary(d => d.Name, d => d.Value);
    }
}
