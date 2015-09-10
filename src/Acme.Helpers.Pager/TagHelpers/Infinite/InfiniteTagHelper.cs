using Acme.Helpers.Core.Extensions;
using Acme.Helpers.Core.Library;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.ModelBinding;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using System;
using System.Threading.Tasks;

namespace Acme.Helpers.TagHelpers
{
    [TargetElement("infinite")]
    public class InfiniteTagHelper : TagHelper, ISupportInfinite, ISupportAnchor, ISupportFor
    {
        [ViewContext]
        public ViewContext ViewContext { get; set; }
        public IUrlHelper UrlHelper { get; set; }

        #region ISupportFor
        /// <inheritDoc/>
        public ModelExpression AspFor { get; set; }
        #endregion

        #region ISupportAnchor
        /// <inheritDoc/>
        public string AspAction { get; set; }
        /// <inheritDoc/>
        public string AspController { get; set; }
        /// <inheritDoc/>
        public string AspProtocol { get; set; }
        /// <inheritDoc/>
        public string AspHost { get; set; }
        /// <inheritDoc/>
        public string AspFragment { get; set; }
        #endregion

        #region ISupportInfinite
        /// <inheritDoc/>
        public string Id { get; set; }
        /// <inheritDoc/>
        public string ReplaceId { get; set; }
        /// <inheritDoc/>
        public string Style { get; set; } = LoadMoreDefaults.Style;
        /// <inheritDoc/>
        public string ContainerStyle { get; set; } = LoadMoreDefaults.ContainerStyle;
        #endregion

        /// <inheritDoc/>
        [HtmlAttributeName(SkipAttributeName)]
        public int Skip { get; set; }
        private const string SkipAttributeName = "skip";

        /// <inheritDoc/>
        [HtmlAttributeName(TotalAttributeName)]
        public int Total { get; set; }
        private const string TotalAttributeName = "total";

        /// <exclude/>
        private const string RouteAttributePrefix = "asp-route-";

        /// <exclude/>
        private string[] PossibleSkipParameterNames = { "Skip" };
        /// <exclude/>
        private string[] PossibleTotalParameterNames = { "Total", "Count" };

        private ModelExplorer Explorer { get
            {
                ModelExplorer explorer = AspFor?.ModelExplorer;
                if (explorer == null)
                    explorer = ViewContext.ViewData.ModelExplorer;
                return explorer;
            }
        }

        public InfiniteTagHelper(IUrlHelper urlHelper)
        {
            UrlHelper = urlHelper;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null;

            ApplyActionAttributes();
            ApplyPaginationAttributes(context);

            //Get the url
            var routeValues = output.TrimPrefixedAttributes(RouteAttributePrefix);
            routeValues.Add("skip", Skip);
            var url = CreateLink(routeValues);

            var replaceId = ReplaceId ?? context.UniqueId;
            var content = (await context.GetChildContentAsync()).ReplaceStringTokens(Explorer);
            if (string.IsNullOrEmpty(content))
                content = StringResources.InfiniteLabelText;

            FluentTagBuilder builder = new FluentTagBuilder()
                .StartTag("div")
                    //if there's no replace id specified, set the newly created div as the replacement area
                    .AttributeIf(string.IsNullOrEmpty(ReplaceId), "Id", replaceId)
                    .AttributeIf(string.IsNullOrEmpty(ReplaceId), "Style", ContainerStyle)
                    .ActionIf(Skip < Total, tag =>
                    {
                        tag.Append(new FluentTagBuilder()
                               .AjaxAnchor(url, "replace-with", replaceId, content, new
                               {
                                   @class = context.AllAttributes["class"]?.Value?.ToString(),
                                   id = Id,
                                   style = Style
                               })
                        );
                    })
                .EndTag();

            output.Content.SetContent(builder);
        }

        private void ApplyActionAttributes()
        {
            //has an action or controller been specified? if not, default
            if (string.IsNullOrEmpty(AspAction))
                AspAction = (string)ViewContext.RouteData.Values["action"];
            if (string.IsNullOrEmpty(AspController))
                AspController = (string)ViewContext.RouteData.Values["controller"];
            if (string.IsNullOrEmpty(AspController))
                throw new ArgumentException($"You must specify the '{nameof(AspController).SplitCamelCase('-').ToLower()}' attribute");
        }

        private void ApplyPaginationAttributes(TagHelperContext context)
        {
            if (!context.AllAttributes.ContainsName(SkipAttributeName) &&
                !context.AllAttributes.ContainsName(TotalAttributeName))
            {
                ModelExplorer explorer = AspFor?.ModelExplorer;
                if (explorer == null)
                    explorer = ViewContext.ViewData.ModelExplorer;

                var skip = explorer.GetExplorerForProperty(PossibleSkipParameterNames);
                var total = explorer.GetExplorerForProperty(PossibleTotalParameterNames);

                if (skip == null || total == null)
                    throw new ArgumentException($"A model MUST contain values for a skip and total.");

                Skip = Convert.ToInt32(skip.Model);
                Total = Convert.ToInt32(total.Model);
            }
        }

        private string CreateLink(object routePrefixedAttributes)
        {
            return UrlHelper.Action(
                AspAction,
                AspController,
                routePrefixedAttributes,
                AspProtocol,
                AspHost,
                AspFragment);
        }
    }
}
