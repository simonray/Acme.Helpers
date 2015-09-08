
namespace Acme.Helpers.TagHelpers
{
    /// <summary>
    /// Attributes available to the table header.
    /// </summary>
    public interface ISupportTableHeader
    {
        /// <summary>
        /// Id to be used by this header element (unique or a model property).
        /// </summary>
        string HeaderId { get; set; }
        /// <summary>
        /// Show or hide the header.
        /// </summary>
        bool? HeaderVisible { get; set; }
        /// <summary>
        /// The display format of the value contained in the cell.
        /// </summary>
        string CellDisplayFormat { get; set; }
        /// <summary>
        /// Set the value to be displayed if the cell value is null.
        /// </summary>
        string CellNullDisplayText { get; set; }
        /// <summary>
        /// Apply a specific display template to the cell value.
        /// </summary>
        string CellUihint { get; set; }
    }
}
