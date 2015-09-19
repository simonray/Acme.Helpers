using Acme.Helpers.Demo.Internal;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using System.Threading.Tasks;

namespace Acme.Helpers.TagHelpers
{
    [TargetElement(TagName.NugetPanel, Attributes = PackageAttributeName)]
    public class NugetPanelTagHelper : TagHelper
    {
        [HtmlAttributeName(PackageAttributeName)]
        public string Package { get; set; }
        private const string PackageAttributeName = "package";

        [HtmlAttributeName(RepositoryAttributeName)]
        public string Repository { get; set; }
        private const string RepositoryAttributeName = "repository";

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null;
            output.TagMode = TagMode.SelfClosing;
            output.Content.SetContent(
                NugetGenerator.GenerateNugetPanel(Package, (await context.GetChildContentAsync()).ToString(), Repository)
            );
        }
    }
}
