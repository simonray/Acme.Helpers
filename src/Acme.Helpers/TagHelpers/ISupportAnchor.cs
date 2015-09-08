
namespace Acme.Helpers.TagHelpers
{
    /// <summary>
    /// Attributes required to support anchors.
    /// </summary>
    public interface ISupportAnchor
    {
        /// <summary>
        /// The name of the action method.
        /// </summary>
        /// <remarks>Must be <c>null</c> if <see cref="Route"/> is non-<c>null</c>.</remarks>
        string AspAction { get; set; }
        /// <summary>
        /// Gets or sets the controller.
        /// </summary>
        string AspController { get; set; }
        /// <summary>
        /// The protocol for the URL, such as &quot;http&quot; or &quot;https&quot;.
        /// </summary>
        string AspProtocol { get; set; }
        /// <summary>
        /// The host name.
        /// </summary>
        string AspHost { get; set; }
        /// <summary>
        /// The URL fragment name.
        /// </summary>
        string AspFragment { get; set; }
    }
}
