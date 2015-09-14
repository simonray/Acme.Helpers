
namespace Acme.Helpers.TagHelpers
{
    /// <summary>
    /// Attributes available to the table header.
    /// </summary>
    public interface ISupportTableHeader
    {
        /// <summary>
        /// Gets or sets the model expression.
        /// </summary>
        string AspFor { get; set; }

        /// <summary>
        /// Get or set the standard style property.
        /// </summary>
        string Style { get; set; }

        /// <summary>
        /// Get or set the standard width property.
        /// </summary>
        string Width { get; set; }

        /// <summary>
        /// Id to be used by this header element (unique or a model property).
        /// </summary>
        string HeaderId { get; set; }

        /// <summary>
        /// Header title.
        /// </summary>
        string HeaderTitle { get; set; }

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
        /// <summary>
        /// Is the column is visible or not.
        /// </summary>
        bool? CellVisible { get; set; }
        /// <summary>
        /// Content of the cell. This can contain tokens that relate to the properties available on the given row.
        /// <code>
        /// asp-route-id="{{@nameof(BasicPersonView.Id)}}" or asp-route-id="{{Id}}"
        /// </code>
        /// </summary>
        string CellContent { get; set; }
    }
}
