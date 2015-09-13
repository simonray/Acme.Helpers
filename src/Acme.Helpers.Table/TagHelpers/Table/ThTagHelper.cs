using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acme.Helpers.TagHelpers
{
    [TargetElement("th", Attributes = ForAttributeName)]
    public class ThTagHelper : TagHelper, ISupportTableHeader
    {
        public string Style { get; set; }
        public string Width { get; set; }
        public string Align { get; set; }

        [HtmlAttributeName(ForAttributeName)]
        public string AspFor { get; set; }
        private const string ForAttributeName = "asp-for";

        #region ISupportTableHeader
        [HtmlAttributeName(IdAttributeName)]
        public string HeaderId { get; set; }
        private const string IdAttributeName = "id";
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
        #endregion

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var tableOutput = context.Items["TableOutput"] as IList<TableColumn>;
            if (tableOutput == null)
                return;

            if (string.IsNullOrEmpty(AspFor) && !string.IsNullOrEmpty(CellUihint))
                throw new ArgumentException($"[<th>] You must supply an '{ForAttributeName}' attribute if you are using '{UihintAttributeName}'. This property value will be passed through to the template");

            TagHelperContent content = (await context.GetChildContentAsync());
            tableOutput.Add(new TableColumn(this, content.IsEmpty ? null : content.GetContent()));
        }
    }
}