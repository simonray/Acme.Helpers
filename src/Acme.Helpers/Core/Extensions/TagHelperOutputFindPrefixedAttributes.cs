using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Acme.Helpers.Core.Extensions
{
    /// <exclude/>
    internal static partial class ExtensionMethods
    {
        /// <exclude/>
        public static IDictionary<string, object> FindPrefixedAttributes(this TagHelperOutput @this, string prefix)
            => @this.Attributes
                .Where(attribute => attribute.Name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                .ToDictionary(d => d.Name, d => d.Value);
    }
}
