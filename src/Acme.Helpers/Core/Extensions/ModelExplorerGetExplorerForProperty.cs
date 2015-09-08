using Microsoft.AspNet.Mvc.ModelBinding;

namespace Acme.Helpers.Core.Extensions
{
    /// <exclude/>
    internal static partial class ExtensionMethods
    {
        /// <exclude/>
        public static ModelExplorer GetExplorerForProperty(this ModelExplorer @this, string[] properties)
        {
            foreach(var property in properties)
            {
                var propertyExplorer = @this.GetExplorerForProperty(property);
                if (propertyExplorer != null)
                    return propertyExplorer;
            }
            return null;
        }
    }
}
