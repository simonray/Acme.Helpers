using Acme.Helpers.Demo.Internal;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;

namespace Acme.Helpers.TagHelpers
{
    [TargetElement(TagName.GithubForkme)]
    public class GithubForkmeTagHelper : TagHelper
    {
        public string Repository { get; set; }
        public GithubForkmeColor Color { get; set; } = GithubForkmeColor.Orange;
        public GithubForkmeSide Side { get; set; } = GithubForkmeSide.Right;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null;
            output.Content.SetContent(GithubGenerator.GenerateGithubForkme(Repository, Side, Color));
        }
    }
}
