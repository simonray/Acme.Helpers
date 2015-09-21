using Microsoft.AspNet.Html.Abstractions;
using Microsoft.AspNet.Mvc.Rendering;
using static Acme.Helpers.Demo.Internal.NugetGenerator;

namespace Acme.Helpers.HtmlHelpers
{
    /// <exclude />
    public static class NugetPanelHtmlHelper
    {
        /// <summary>
        /// Returns a standard nuget panel (plus version shield) and optionally github links if a repository has been specified.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="IHtmlHelper"/> Instance this method extends.</param>
        /// <param name="package">Nuget package name.</param>
        /// <param name="description">Information about the package.</param>
        /// <param name="repository">Github repository. If specified, buttons will be added with links for download and source.</param>
        /// <returns>A new <see cref="IHtmlContent"/> containing the created HTML.</returns>
        public static IHtmlContent NugetPanel(this IHtmlHelper htmlHelper, string package, string description, string repository = null)
        {
            return new HtmlString(GenerateNugetPanel(package, description, repository));
        }
    }
}
