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
    public class InfiniteTagHelper : TagHelper, ISupportFor, ISupportAnchor, ISupportInfinite
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
        [HtmlAttributeName(InfiniteIdAttributeName)]
        public string InfiniteId { get; set; }
        private const string InfiniteIdAttributeName = "id";
        /// <inheritDoc/>
        [HtmlAttributeName(InfiniteReplaceIdAttributeName)]
        public string InfiniteReplaceId { get; set; }
        private const string InfiniteReplaceIdAttributeName = "replace-id";
        /// <inheritDoc/>
        [HtmlAttributeName(InfiniteStyleAttributeName)]
        public string InfiniteStyle { get; set; } = LoadMoreDefaults.Style;
        private const string InfiniteStyleAttributeName = "style";
        /// <inheritDoc/>
        [HtmlAttributeName(InfiniteContainerStyleAttributeName)]
        public string InfiniteContainerStyle { get; set; } = LoadMoreDefaults.ContainerStyle;
        private const string InfiniteContainerStyleAttributeName = "container-style";
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

            var replaceId = InfiniteReplaceId ?? context.UniqueId;
            var content = (await context.GetChildContentAsync()).ReplaceStringTokens(Explorer);
            if (string.IsNullOrEmpty(content))
                content = StringResources.InfiniteLabelText;

            FluentTagBuilder builder = new FluentTagBuilder()
                .StartTag("div")
                    //if there's no replace id specified, set the newly created div as the replacement area
                    .AttributeIf(string.IsNullOrEmpty(InfiniteReplaceId), "Id", replaceId)
                    .AttributeIf(string.IsNullOrEmpty(InfiniteReplaceId), "Style", InfiniteContainerStyle)
                    .ActionIf(Skip < Total, tag =>
                    {
                        tag.Append(new FluentTagBuilder()
                               .AjaxAnchor(url, "replace-with", replaceId, content, new
                               {
                                   @class = context.AllAttributes["class"]?.Value?.ToString(),
                                   id = InfiniteId,
                                   style = InfiniteStyle
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
