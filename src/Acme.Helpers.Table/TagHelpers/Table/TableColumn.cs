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
        public string Title { get; set; }
        public string Style { get; set; }
        public bool Visible { get; set; }
        public string Width { get; set; }
        public string CellDisplayFormat { get; set; }
        public string CellNullDisplayText { get; set; }
        public string CellUihint { get; set; }

        public TableColumn()
        {
            Visible = true;
        }

        public TableColumn(ModelMetadata meta)
            : this()
        {
            Id = meta.PropertyName;
            For = meta.PropertyName;
            Title = meta.DisplayName ?? meta.PropertyName.SplitCamelCase();
            Visible = !meta.HideSurroundingHtml;
            CellDisplayFormat = meta.DisplayFormatString;
            CellNullDisplayText = meta.NullDisplayText;
            CellUihint = meta.TemplateHint;
        }

        public TableColumn(ThTagHelper th, string content)
            : this()
        {
            Id = th.HeaderId ?? th.AspFor;
            For = th.AspFor;
            Title = content ?? Id.SplitCamelCase();
            Style = th.Style;
            Visible = th.HeaderVisible ?? true;
            Width = th.Width;

            CellDisplayFormat = th.CellDisplayFormat;
            CellNullDisplayText = th.CellNullDisplayText;
            CellUihint = th.CellUihint;
        }

        internal void Merge(TableColumn col)
        {
            Title = col.Title ?? Title;
            CellDisplayFormat = col.CellDisplayFormat ?? CellDisplayFormat;
            CellNullDisplayText = col.CellNullDisplayText ?? CellNullDisplayText;
            CellUihint = col.CellUihint ?? CellUihint;
        }
    }
}
