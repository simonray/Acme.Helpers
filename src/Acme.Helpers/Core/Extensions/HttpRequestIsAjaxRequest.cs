using Microsoft.AspNet.Http;

namespace Acme.Helpers.Core.Extensions
{
    /// <exclude/>
    internal static partial class ExtensionMethods
    {
        /// <exclude/>
        public static bool IsAjaxRequest(this HttpRequest @this)
            => @this?.Headers?["X-Requested-With"] == "XMLHttpRequest";
    }
}
