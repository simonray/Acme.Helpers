using Acme.Helpers.Core.Extensions;
using Microsoft.AspNet.Mvc.ModelBinding;
using System.Diagnostics;

namespace Acme.Helpers.TagHelpers
{
    [DebuggerDisplayAttribute("{Id}")]
    internal class TableColumn
    {
        public string Id { get; set; }
        public string For { get; set; }
        public int Order { get; set; }
        public string Title { get; set; }
        public string Style { get; set; }
        public string Width { get; set; }
        public string CellContent { get; set; }
        public string CellDisplayFormat { get; set; }
        public string CellNullDisplayText { get; set; }
        public string CellUihint { get; set; }
        public bool CellVisible { get; set; }

        public TableColumn()
        {
            CellVisible = true;
        }

        public TableColumn(ModelMetadata meta)
            : this()
        {
            Id = meta.PropertyName;
            For = meta.PropertyName;
            Order = meta.Order;
            Title = meta.DisplayName ?? meta.PropertyName.SplitCamelCase();

            CellDisplayFormat = meta.DisplayFormatString;
            CellNullDisplayText = meta.NullDisplayText;
            CellUihint = meta.TemplateHint;
            CellVisible = !meta.HideSurroundingHtml;
        }

        public TableColumn(ISupportTableHeader th)
            : this()
        {
            Id = th.HeaderId ?? th.AspFor;
            For = th.AspFor;
            Order = th.HeaderOrder;
            Title = th.HeaderTitle ?? Id.SplitCamelCase();
            Style = th.Style;
            Width = th.Width;

            CellDisplayFormat = th.CellDisplayFormat;
            CellNullDisplayText = th.CellNullDisplayText;
            CellUihint = th.CellUihint;
            CellVisible = th.CellVisible ?? true;
            CellContent = th.CellContent;
        }

        internal void Merge(TableColumn col)
        {
            Title = col.Title ?? Title;
            CellDisplayFormat = col.CellDisplayFormat ?? CellDisplayFormat;
            CellNullDisplayText = col.CellNullDisplayText ?? CellNullDisplayText;
            CellUihint = col.CellUihint ?? CellUihint;
            CellVisible = col.CellVisible;
            CellContent = col.CellContent;
        }
    }
}
