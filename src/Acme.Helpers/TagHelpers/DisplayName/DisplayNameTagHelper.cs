using Acme.Helpers.Core.Extensions;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using System.Threading.Tasks;

namespace Acme.Helpers.TagHelpers
{
    [TargetElement(TagName.DisplayName, Attributes = ForAttributeName)]
    public class DisplayNameTagHelper : TagHelper, ISupportFor
    {
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }
        [HtmlAttributeNotBound]
        protected IHtmlHelper HtmlHelper { get; set; }

        #region ISupportFor
        /// <inheritDoc/>
        public ModelExpression AspFor { get; set; }
        private const string ForAttributeName = "asp-for";
        #endregion

        /// <summary>
        /// Specify whether the name should be split on case (default: true).
        /// </summary>
        public bool Split { get; set; } = true;

        public DisplayNameTagHelper(IHtmlHelper htmlHelper)
        {
            HtmlHelper = htmlHelper;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            (HtmlHelper as ICanHasViewContext)?.Contextualize(ViewContext);

            output.TagName = null;
            if (Split)
                output.Content.SetContent(HtmlHelper.DisplayName(AspFor.Metadata.PropertyName).SplitCamelCase());
            else
                output.Content.SetContent(HtmlHelper.DisplayName(AspFor.Metadata.PropertyName));
            output.Content.Append(await context.GetChildContentAsync());
        }
    }
}