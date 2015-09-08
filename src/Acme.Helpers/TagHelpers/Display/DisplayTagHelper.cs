using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using System.Threading.Tasks;

namespace Acme.Helpers.TagHelpers
{
    [TargetElement("display", Attributes = ForAttributeName)]
    public class DisplayTagHelper : TagHelper, ISupportFor
    {
        [HtmlAttributeNotBound, ViewContext]
        public ViewContext ViewContext { get; set; }

        [HtmlAttributeNotBound]
        protected IHtmlHelper HtmlHelper { get; set; }

        #region ISupportFor
        /// <inheritDoc/>
        public ModelExpression AspFor { get; set; }
        private const string ForAttributeName = "asp-for";
        #endregion

        public DisplayTagHelper(IHtmlHelper htmlHelper)
        {
            HtmlHelper = htmlHelper;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            (HtmlHelper as ICanHasViewContext)?.Contextualize(ViewContext);
            output.TagName = null;

            if (AspFor.Metadata?.TemplateHint != null)
                output.Content.SetContent(
                    HtmlHelper.Partial($"{Const.DisplayTemplateViewPath}/{AspFor.Metadata.TemplateHint}", AspFor.Model).ToString());
            else
                output.Content.SetContent(
                    string.Format(System.Globalization.CultureInfo.CurrentCulture,
                        AspFor.Metadata?.DisplayFormatString ?? "{0}", AspFor.Model));

            output.Content.Append(await context.GetChildContentAsync());
        }
    }
}
