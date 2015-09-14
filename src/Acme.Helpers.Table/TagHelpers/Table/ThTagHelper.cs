using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acme.Helpers.TagHelpers
{
    [TargetElement("th", Attributes = ForAttributeName)]
    public class ThTagHelper : TagHelper, ISupportTableHeader
    {
        #region ISupportTableHeader
        /// <inheritDoc/>
        public string Style { get; set; }
        /// <inheritDoc/>
        public string Width { get; set; }
        /// <inheritDoc/>
        [HtmlAttributeName(ForAttributeName)]
        public string AspFor { get; set; }
        private const string ForAttributeName = "asp-for";
        [HtmlAttributeName(IdAttributeName)]
        public string HeaderId { get; set; }
        private const string IdAttributeName = "id";
        [HtmlAttributeNotBound]
        public string HeaderTitle { get; set; } // tag content
        [HtmlAttributeName(CellDisplayFormatAttributeName)]
        public string CellDisplayFormat { get; set; }
        private const string CellDisplayFormatAttributeName = "cell-display-format";
        [HtmlAttributeName(NullDisplayTextAttributeName)]
        public string CellNullDisplayText { get; set; }
        private const string NullDisplayTextAttributeName = "cell-null-display-text";
        [HtmlAttributeName(UihintAttributeName)]
        public string CellUihint { get; set; }
        private const string UihintAttributeName = "cell-uihint";
        [HtmlAttributeName(CellVisibleAttributeName)]
        public bool? CellVisible { get; set; }
        private const string CellVisibleAttributeName = "cell-visible";
        [HtmlAttributeName(ContentVisibleAttributeName)]
        public string CellContent { get; set; } // tag attribute
        private const string ContentVisibleAttributeName = "cell-content";
        #endregion

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var tableOutput = context.Items["TableOutput"] as IList<TableColumn>;
            if (tableOutput == null)
                return;

            if (string.IsNullOrEmpty(AspFor) && !string.IsNullOrEmpty(CellUihint))
                throw new ArgumentException($"[<th>] You must supply an '{ForAttributeName}' attribute if you are using '{UihintAttributeName}'. This property value will be passed through to the template");

            TagHelperContent content = (await context.GetChildContentAsync());
            HeaderTitle = content.IsEmpty ? null : content.GetContent();
            tableOutput.Add(new TableColumn(this));
        }
    }
}