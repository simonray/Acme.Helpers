using System.Threading.Tasks;
using Acme.Helpers.Demo.Internal;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;

namespace Acme.Helpers.TagHelpers
{
    [TargetElement(TagName.NugetCommand, Attributes = PackageAttributeName)]
    public class NugetCommandTagHelper : TagHelper
    {
        [HtmlAttributeName(PackageAttributeName)]
        public string Package { get; set; }
        private const string PackageAttributeName = "package";

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null;

            output.Content.SetContent(
                NugetGenerator.GenerateNugetCommand(Package,
                output.TagMode == TagMode.SelfClosing,
                (await context.GetChildContentAsync()).GetContent()));

            output.TagMode = TagMode.SelfClosing;
        }
    }
}
