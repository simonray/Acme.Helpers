using Microsoft.AspNet.Html.Abstractions;
using Microsoft.AspNet.Mvc.Rendering;
using static Acme.Helpers.Demo.Internal.GithubGenerator;

namespace Acme.Helpers.HtmlHelpers
{
    /// <exclude />
    public static class GithubForkmeHtmlHelper
    {
        /// <summary>
        /// Returns the standard Github Forkme image banner.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="IHtmlHelper"/> Instance this method extends.</param>
        /// <param name="repository">Github repository that will be used in the anchor.</param>
        /// <param name="side">The side of the window that the banner will be displayed.</param>
        /// <param name="color">The color of the banner.</param>
        /// <returns>A new <see cref="IHtmlContent"/> containing the created HTML.</returns>
        public static IHtmlContent GithubForkme(this IHtmlHelper htmlHelper, string repository, GithubForkmeSide side = GithubForkmeSide.Right, GithubForkmeColor color = GithubForkmeColor.Gray)
        {
            return new HtmlString(GenerateGithubForkme(repository, side, color));
        }
    }
}
