using Microsoft.AspNet.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Acme.Helpers.Core.Extensions
{
    /// <exclude/>
    internal static partial class ExtensionMethods
    {
        /// <exclude/>
        public static IDictionary<string, object> GetHtmlAttributeDictionary(this object @this)
        {
            IDictionary<string, object> htmlAttributeDictionary = new Dictionary<string, object>();
            if (@this != null)
            {
                htmlAttributeDictionary = @this as IDictionary<string, object>;
                if (htmlAttributeDictionary == null)
                {
                    htmlAttributeDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(@this);
                }
            }

            return htmlAttributeDictionary;
        }
    }
}
