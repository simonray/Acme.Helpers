using Acme.Helpers.Core.Extensions;
using Acme.Helpers.Core.Library;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Acme.Helpers.TagHelpers
{
    public abstract class BasePagerTagHelper : TagHelper, ISupportAnchor
    {
        private const string RouteAttributePrefix = "asp-route-";
        private const string AjaxAttributePrefix = "data-ajax";

        /// <exclude/>
        [HtmlAttributeNotBound, ViewContext]
        public ViewContext ViewContext { get; set; }
        /// <exclude/>
        [HtmlAttributeNotBound]
        protected IUrlHelper UrlHelper { get; set; }

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

        [HtmlAttributeNotBound]
        protected IDictionary<string, object> RouteValues { get; private set; }
        [HtmlAttributeNotBound]
        protected IDictionary<string, object> AjaxValues { get; private set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            ApplyActionAttributes(context);
            if (string.IsNullOrEmpty(AspController))
                throw new ArgumentException($"<{output.TagName}> You must specify the '{nameof(AspController).SplitCamelCaseLowerDash()}' attribute");

            await base.ProcessAsync(context, output);
        }

        private void ApplyActionAttributes(TagHelperContext context)
        {
            //has an action or controller been specified? if not, default
            if (string.IsNullOrEmpty(AspAction))
                AspAction = (string)ViewContext.RouteData.Values["action"];
            if (string.IsNullOrEmpty(AspController))
                AspController = (string)ViewContext.RouteData.Values["controller"];

            RouteValues = context.TrimPrefixedAttributes(RouteAttributePrefix);
            AjaxValues = context.FindPrefixedAttributes(AjaxAttributePrefix);
        }
    }
}
