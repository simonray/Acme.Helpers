using Microsoft.AspNet.Mvc.Rendering;

namespace Acme.Helpers.TagHelpers
{
    /// <summary>
    /// Attributes required to support model expressions.
    /// </summary>
    public interface ISupportFor
    {
        /// <summary>
        /// Gets or sets the model expression.
        /// </summary>
        ModelExpression AspFor { get; set; }
    }
}
