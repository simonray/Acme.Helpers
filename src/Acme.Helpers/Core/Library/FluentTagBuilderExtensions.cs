
namespace Acme.Helpers.Core.Library
{
    internal static class FluentTagBuilderExtensions
    {
        /// <summary>
        /// Set the class attribute.
        /// </summary>
        public static FluentTagBuilder Class(this FluentTagBuilder @this, string value)
            => @this.Attribute("class", value);

        /// <summary>
        /// Set the  style attribute.
        /// </summary>
        public static FluentTagBuilder Style(this FluentTagBuilder @this, string value)
            => @this.Attribute("style", value);

        /// <summary>
        /// Set the type attribute.
        /// </summary>
        public static FluentTagBuilder Type(this FluentTagBuilder @this, string value)
            => @this.Attribute("type", value);

        /// <summary>
        /// Create a new tag.
        /// </summary>
        public static FluentTagBuilder Tag(this FluentTagBuilder @this, string tagName, string content = null, object htmlAttributes = null)
        {
            return @this
                .StartTag(tagName)
                    .AppendIf(content != null, tag => { return new FluentTagBuilder().Append(content); })
                    .Attributes(htmlAttributes)
                .EndTag();
        }

        /// <summary>
        /// Create a new anchor.
        /// </summary>
        public static FluentTagBuilder Anchor(this FluentTagBuilder @this, string href, string content, object htmlAttributes = null)
        {
            return @this.Append(new FluentTagBuilder()
                 .StartTag("a")
                     .Attribute("href", href)
                     .Attributes(htmlAttributes)
                     .Append(content)
                 .EndTag());
        }

        /// <summary>
        /// Create a new ajax anchor.
        /// </summary>
        public static FluentTagBuilder AjaxAnchor(this FluentTagBuilder @this, string href, string mode, string updateId, string content = null, object htmlAttributes = null)
        {
            return @this.Append(new FluentTagBuilder()
                .StartTag("a")
                    .Attribute("href", href)
                    .Attribute("data-ajax", "true")
                    .Attribute("data-ajax-mode", mode)
                    .Attribute("data-ajax-update", $"#{updateId}")
                    .Attributes(htmlAttributes)
                    .Append(content)
                .EndTag());
        }

        /// <summary>
        /// Create a new divider.
        /// </summary>
        public static FluentTagBuilder Div(this FluentTagBuilder @this, string classAttr = null, string content = null)
        {
            return @this.Append(new FluentTagBuilder()
                .StartTag("div").ActionIf(!string.IsNullOrEmpty(classAttr), tag => tag.Class(classAttr))
                    .Append(content)
                .EndTag());
        }
    }
}
