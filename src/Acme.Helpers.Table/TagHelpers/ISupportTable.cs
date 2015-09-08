
namespace Acme.Helpers.TagHelpers
{
    /// <summary>
    /// Attributes available to the table.
    /// </summary>
    internal interface ISupportTable
    {
        /// <summary>
        /// Whether the table should generate columns automatically based on the model source.
        /// </summary>
        bool TableAutoGenerateColumns { get; set; }
        /// <summary>
        /// Message displayed when there are no records in the enumeration.
        /// </summary>
        string TableNoRecordsMessage { get; set; }
        /// <summary>
        /// Whether the table generate use it's own pager.
        /// </summary>
        bool TablePagination { get; set; }
        /// <summary>
        /// Whether the table show a header or not.
        /// </summary>
        bool TableShowHeader { get; set; }
        /// <summary>
        /// Support ajax.
        /// </summary>
        bool TableAjax { get; set; }
    }
}
