using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Acme.Helpers.TagHelpers
{
    /// <summary>
    /// Attributes required to support the infinite pager.
    /// </summary>
    internal interface ISupportInfinite
    {
        /// <summary>
        /// Style attribute set against the pagers container.
        /// </summary>
        string InfiniteContainerStyle { get; set; }
        /// <summary>
        /// Id that will be assocated to anchor/button control. If none is specified one will be auto-generated.
        /// </summary>
        string InfiniteId { get; set; }
        /// <summary>
        /// Message displayed when there are no records in the enumeration.
        /// </summary>
        string InfiniteNoRecordsMessage { get; set; }
        /// <summary>
        /// Id of an element where the result of the load more action will be inserted/replaced.
        /// </summary>
        string InfiniteReplaceId { get; set; }
        /// <summary>
        /// Style attribute set against the pager.
        /// </summary>
        string InfiniteStyle { get; set; }
    }
}
