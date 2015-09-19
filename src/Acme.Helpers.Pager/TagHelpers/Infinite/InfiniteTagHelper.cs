using Acme.Helpers.Core.Extensions;
using Acme.Helpers.Core.Library;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.ModelBinding;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acme.Helpers.TagHelpers
{
    [TargetElement(TagName.Infinite)]
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
        [HtmlAttributeName(InfiniteContainerStyleAttributeName)]
        public string InfiniteContainerStyle { get; set; } = LoadMoreDefaults.ContainerStyle;
        private const string InfiniteContainerStyleAttributeName = "container-style";
        /// <inheritDoc/>
        [HtmlAttributeName(InfiniteIdAttributeName)]
        public string InfiniteId { get; set; }
        private const string InfiniteIdAttributeName = "id";
        /// <inheritDoc/>
        [HtmlAttributeName(InfiniteNoRecordsMessageAttributeName)]
        public string InfiniteNoRecordsMessage { get; set; } = StringResources.TableNoRecordsText;
        private const string InfiniteNoRecordsMessageAttributeName = "no-records-message";
        /// <inheritDoc/>
        [HtmlAttributeName(InfiniteReplaceIdAttributeName)]
        public string InfiniteReplaceId { get; set; }
        private const string InfiniteReplaceIdAttributeName = "replace-id";
        /// <inheritDoc/>
        [HtmlAttributeName(InfiniteStyleAttributeName)]
        public string InfiniteStyle { get; set; } = LoadMoreDefaults.Style;
        private const string InfiniteStyleAttributeName = "style";
        #endregion

        [HtmlAttributeNotBound]
        private IDictionary<string, object> RouteValues { get; set; }
        [HtmlAttributeNotBound]
        private IDictionary<string, object> AjaxValues { get; set; }

        /// <inheritDoc/>
        [HtmlAttributeName(SkipAttributeName)]
        public int Skip { get; set; }
        private const string SkipAttributeName = "skip";

        /// <inheritDoc/>
        [HtmlAttributeName(TotalAttributeName)]
        public int Total { get; set; }
        private const string TotalAttributeName = "total";

        /// <exclude/>
        private string[] PossibleSkipParameterNames = { "Skip", "Jump" };
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

            ApplyActionAttributes(context);
            if (string.IsNullOrEmpty(AspController))
                throw new ArgumentException($"<{output.TagName}> You must specify the '{nameof(AspController).SplitCamelCaseLowerDash()}' attribute");

            ApplyPaginationAttributes(context);

            FluentTagBuilder builder = new FluentTagBuilder();
            if (Total == 0)
                builder.Append(InfiniteNoRecordsMessage);
            else
                builder = await Create(context, RouteValues);

            output.Content.SetContent(builder);
        }

        private void ApplyActionAttributes(TagHelperContext context)
        {
            //has an action or controller been specified? if not, default
            if (string.IsNullOrEmpty(AspAction))
                AspAction = (string)ViewContext.RouteData.Values["action"];
            if (string.IsNullOrEmpty(AspController))
                AspController = (string)ViewContext.RouteData.Values["controller"];

            RouteValues = context.TrimPrefixedAttributes(Const.RouteAttributePrefix);
            AjaxValues = context.FindPrefixedAttributes(Const.AjaxAttributePrefix);
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

        private async Task<FluentTagBuilder> Create(TagHelperContext context, System.Collections.Generic.IDictionary<string, object> routeValues)
        {
            var url = CreateLink(routeValues);
            routeValues.Add("skip", Skip);

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
            return builder;
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
