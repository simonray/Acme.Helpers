using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Acme.Helpers.TagHelpers
{
    /// <summary>
    /// Attributes required to support the load more pager.
    /// </summary>
    internal interface ISupportInfinite
    {
        /// <summary>
        /// Id that will be assocated to anchor/button control. If none is specified one will be auto-generated.
        /// </summary>
        string Id { get; set; }
        /// <summary>
        /// Id of an element where the result of the load more action will be inserted/replaced.
        /// </summary>
        string ReplaceId { get; set; }
        /// <summary>
        /// Style attribute set against the pager.
        /// </summary>
        string Style { get; set; }
        /// <summary>
        /// Style attribute set against the pagers container.
        /// </summary>
        string ContainerStyle { get; set; }
    }
}
