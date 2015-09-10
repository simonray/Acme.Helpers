using Acme.Helpers.Core.Extensions;
using Acme.Helpers.Core.Library;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.ModelBinding;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Acme.Helpers.TagHelpers
{
    /// <summary>
    /// <see cref="ITagHelper"/> Implementation targeting &lt;table&gt; elements with an <c>asp-for</c> attribute.
    /// </summary>
    [TargetElement("table", Attributes = ForAttributeName)]
    public class TableTagHelper : TagHelper, ISupportTable
    {
        ///<exclude/>
        [HtmlAttributeNotBound, ViewContext]
        public ViewContext ViewContext { get; set; }
        ///<exclude/>
        [HtmlAttributeNotBound]
        protected IHtmlHelper HtmlHelper { get; set; }
        ///<exclude/>
        [HtmlAttributeNotBound]
        protected IModelMetadataProvider MetadataProvider { get; set; }
        ///<exclude/>
        [HtmlAttributeNotBound]
        protected IUrlHelper UrlHelper { get; set; } //only required for pager

        #region ISupportFor
        /// <inheritDoc/>
        [HtmlAttributeName(ForAttributeName)]
        public ModelExpression AspFor { get; set; }
        protected const string ForAttributeName = "asp-for";
        #endregion

        #region ISupportTable
        /// <inheritDoc/>
        [HtmlAttributeName(TableAutoGenerateColumnsAttributeName)]
        public Boolean TableAutoGenerateColumns { get; set; } = TableDefaults.AutoGenerateColumns;
        private const string TableAutoGenerateColumnsAttributeName = "auto-generate-columns";
        /// <inheritDoc/>
        [HtmlAttributeName(ShowHeaderAttributeName)]
        public bool TableShowHeader { get; set; } = TableDefaults.ShowHeader;
        private const string ShowHeaderAttributeName = "show-header";
        /// <inheritDoc/>
        [HtmlAttributeName(TableNoRecordsMessageAttributeName)]
        public string TableNoRecordsMessage { get; set; } = StringResources.TableNoRecordsText;
        private const string TableNoRecordsMessageAttributeName = "no-records-message";
        /// <inheritDoc/>
        [HtmlAttributeName(PagerAttributeName)]
        public bool TablePagination { get; set; } = TableDefaults.Pagination;
        private const string PagerAttributeName = "pagination";
        /// <inheritDoc/>
        [HtmlAttributeName(AjaxAttributeName)]
        public bool TableAjax { get; set; } = TableDefaults.Ajax;
        private const string AjaxAttributeName = "ajax";
        #endregion

        public TableTagHelper(IHtmlHelper htmlHelper, IModelMetadataProvider metadataProvider, IUrlHelper urlHelper)
        {
            HtmlHelper = htmlHelper;
            MetadataProvider = metadataProvider;
            UrlHelper = urlHelper;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            (HtmlHelper as ICanHasViewContext)?.Contextualize(ViewContext);
            var uniqueId = context.UniqueId;

            if (!AspFor.Metadata.IsCollectionType)
                throw new ArgumentNullException($"Check '{ForAttributeName}' is of type {nameof(IEnumerable)} and a '@model' has been defined in the view.");

            IList<TableColumn> columns = new List<TableColumn>();
            if (TableAutoGenerateColumns == true)
                columns = await GetColumnsFromModel(context);
            else
                columns = await GetColumnsFromMarkup(context);

            output.TagName = null;
            output.Content.SetContent(CreateTable(context, columns, uniqueId));
        }

        private FluentTagBuilder CreateTable(TagHelperContext context, IList<TableColumn> columns, string uniqueId)
        {
            return new FluentTagBuilder()
                .StartTag("div").Attribute("id", uniqueId)
                    .StartTag("table", context.AllAttributes.FirstOrDefault(a => a.Name == "class")?.Value?.ToString())
                        .AppendIf(TableShowHeader, tag => { return CreateHeader(columns); })
                        .Append(CreateBody(columns))
                    .EndTag()
                    .ActionIf(TablePagination, async tag =>
                    {
                        tag.Append(await CreatePager(context, uniqueId));
                    })
                .EndTag();
        }

        private FluentTagBuilder CreateHeader(IList<TableColumn> columns)
        {
            return new FluentTagBuilder()
                .StartTag("tr")
                    .Action((Action<FluentTagBuilder>)(tag =>
                    {
                        foreach (var col in columns)
                        {
                            tag.StartTag("th")
                                .CombineAttribute("width", col.Width)
                                .CombineAttribute("style", col.Style)
                                .CombineAttributeIf(!col.CellVisible, "style", "display: none")
                                .Append(GetHeaderValue(col))
                            .EndTag();
                        }
                    }))
                .EndTag();
        }

        private FluentTagBuilder CreateBody(IList<TableColumn> columns)
        {
            return new FluentTagBuilder()
                .StartTag("tbody")
                .Action(rows =>
                {
                    if (((IEnumerable)AspFor.Model).HasRows())
                    {
                        foreach (var row in (IEnumerable)AspFor.Model)
                            rows.StartTag("tr")
                                .Action(cols =>
                                {
                                    foreach (var col in columns)
                                    {
                                        cols.StartTag("td")
                                            .CombineAttribute("width", col.Width)
                                            .CombineAttribute("style", col.Style)
                                            .CombineAttributeIf(!col.CellVisible, "style", "display: none")
                                            .Append(GetColumnValue(row, col))
                                        .EndTag();
                                    }
                                })
                            .EndTag();
                    }
                    else
                    {
                        rows.StartTag("tr")
                            .StartTag("td")
                                .Attribute("colspan", columns.Count.ToString())
                                .Attribute("align", "center")
                                .Append(TableNoRecordsMessage)
                            .EndTag()
                        .EndTag();
                    }
                })
                .EndTag();
        }

        private async Task<string> CreatePager(TagHelperContext context, string uniqueId)
        {
            PagerTagHelper pager = new PagerTagHelper(UrlHelper);
            TagHelperOutput helper = new TagHelperOutput("pager", new TagHelperAttributeList());
            if (TableAjax)
            {
                helper.Attributes.Add("data-ajax-update", $"#{uniqueId}");
                helper.Attributes.Add("data-ajax-mode", "replace");
                helper.Attributes.Add("data-ajax", "true");
            }
            this.MapPropertiesByName(pager);
            pager.PagerShowStatus = TableDefaults.PagerShowSizes;
            pager.PagerShowSizes = TableDefaults.PagerShowStatus;

            await pager.ProcessAsync(context, helper);
            return helper.Content.GetContent();
        }

        private string GetHeaderValue(TableColumn col)
        {
            if (col.Title != null)
                return col.Title;
            return col.Id.SplitCamelCase();
        }

        private string GetColumnValue(object row, TableColumn col)
        {
            string result = string.Empty;

            var propertyExplorer = AspFor.ModelExplorer.GetExplorerForModel(row).GetExplorerForProperty(col.For);
            if (propertyExplorer == null)
                throw new ArgumentException($"Model does not contain column '{col.For}'");

            object value = propertyExplorer.Model;
            if (!string.IsNullOrEmpty(col.CellDisplayFormat))
                result = string.Format(System.Globalization.CultureInfo.CurrentCulture, col.CellDisplayFormat, value);
            else if (!string.IsNullOrEmpty(col.CellUihint))
                result = HtmlHelper.Partial($"{Const.DisplayTemplateViewPath}/{col.CellUihint}", value).ToString();
            else
                result = value?.ToString();

            if (result == null)
            {
                if (col.CellNullDisplayText != null)
                    result = col.CellNullDisplayText;
                else
                    result = string.Empty;
            }

            return result;
        }

        private async Task<IList<TableColumn>> GetColumnsFromModel(TagHelperContext context)
        {
            var columns = await Task.FromResult(GetVisibleColumnsFromModel().ToList());

            var overrides = await GetColumnsFromMarkup(context);
            foreach (var col in overrides)
            {
                TableColumn findColumn = columns.Find(p => p.Id == col.Id);
                if (findColumn != null)
                    findColumn.Merge(col);
                else
                    columns.Add(col);
            }

            return columns;
        }

        private IEnumerable<TableColumn> GetVisibleColumnsFromModel(bool ignoreComplexTypes = false)
        {
            var element = AspFor.Metadata.ModelType.GetGenericArguments().First();
            foreach (var meta in MetadataProvider.GetMetadataForProperties(element).OrderBy(p => p.Order))
            {
                if (!meta.HideSurroundingHtml)
                    if (!ignoreComplexTypes || !meta.IsComplexType)
                        yield return new TableColumn(meta);
            }
        }

        private async Task<IList<TableColumn>> GetColumnsFromMarkup(TagHelperContext context)
        {
            context.Items.Add("TableOutput", new List<TableColumn>());
            await context.GetChildContentAsync();
            return context.Items["TableOutput"] as IList<TableColumn>;
        }
    }
}
