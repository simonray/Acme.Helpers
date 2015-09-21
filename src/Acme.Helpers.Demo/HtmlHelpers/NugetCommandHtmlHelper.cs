using Microsoft.AspNet.Html.Abstractions;
using Microsoft.AspNet.Mvc.Rendering;
using static Acme.Helpers.Demo.Internal.NugetGenerator;

namespace Acme.Helpers.HtmlHelpers
{
    /// <exclude />
    public static class NugetCommandHtmlHelper
    {
        /// <summary>
        /// Returns a standard nuget panel (plus version shield) and optionally github links if a repository has been specified.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="IHtmlHelper"/> Instance this method extends.</param>
        /// <param name="package">Nuget package name.</param>
        /// <param name="ignoreMessage">Show or hide the message.</param>
        /// <param name="message">Install information about the package. If none is specified the standard message
        /// "To install, run the following command in the Package Manager Console" will be used unless 
        /// <paramref name="ignoreMessage"/> is set to false.</param>
        /// <returns>A new <see cref="IHtmlContent"/> containing the created HTML.</returns>
        public static IHtmlContent NugetCommand(this IHtmlHelper htmlHelper, string package, bool ignoreMessage = false, string message = null)
        {
            return new HtmlString(GenerateNugetCommand(package, ignoreMessage, message));
        }
    }
}
