using Acme.Helpers.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acme.Helpers.Core.Library
{
    /// <summary>
    /// Fluent tag builder
    /// </summary>
    /// <remarks>Based on http://www.wiktorzychla.com/2012/02/simple-fluent-and-recursive-tag-builder.html ></remarks>
    internal class FluentTagBuilder
    {
        private string _tagName;
        private FluentTagBuilder _parent;
        private StringBuilder _body = new StringBuilder();
        private IDictionary<string, string> _attributes = new Dictionary<string, string>();

        public FluentTagBuilder() { }

        public FluentTagBuilder Append(string content)
        {
            if (!string.IsNullOrEmpty(content))
                _body.Append(content);
            return this;
        }

        public FluentTagBuilder AppendIf(bool condition, string content)
        {
            if (condition)
                return Append(content);
            else
                return this;
        }

        public FluentTagBuilder Append(Func<FluentTagBuilder, FluentTagBuilder> predicate)
            => Append(predicate(this));

        public FluentTagBuilder AppendIf(bool condition, Func<FluentTagBuilder, string> predicate)
        {
            if (condition)
                return Append(predicate(this));
            else
                return this;
        }

        public FluentTagBuilder StartTag(string tagName, string classAttribute = null)
        {
            FluentTagBuilder tag = new FluentTagBuilder();
            tag._tagName = tagName;
            tag._parent = this;
            if (!string.IsNullOrEmpty(classAttribute))
                tag.Attribute("class", classAttribute.Trim());
            return tag;
        }

        public FluentTagBuilder EndTag()
        {
            if (_parent != null)
            {
                _parent.Append(this.ToString());
                return _parent;
            }
            return this;
        }

        public FluentTagBuilder Attributes(object htmlAttributes)
        {
            var attribs = htmlAttributes.GetHtmlAttributeDictionary();
            foreach (var attrib in attribs)
            {
                Attribute(attrib.Key, attrib.Value?.ToString());
            }
            return this;
        }

        public FluentTagBuilder ActionIf(bool condition, System.Action<FluentTagBuilder> action)
        {
            if (condition)
                Action(action);
            return this;
        }

        public FluentTagBuilder Action(System.Action<FluentTagBuilder> action)
        {
            action(this);
            return this;
        }

        public FluentTagBuilder Attribute(string name, string value)
        {
            _attributes.Add(name, value);
            return this;
        }

        public FluentTagBuilder AttributeIf(bool condition, string name, string value)
        {
            if (condition)
                _attributes.Add(name, value);
            return this;
        }

        public FluentTagBuilder AttributeIfElse(bool condition, string name, string value, string value2)
        {
            if (condition)
                _attributes.Add(name, value);
            else
                _attributes.Add(name, value2);
            return this;
        }

        public FluentTagBuilder CombineAttributeIf(bool condition, string name, string value)
        {
            if (condition)
                CombineAttribute(name, value);
            return this;
        }

        public FluentTagBuilder CombineAttribute(string name, string value)
        {
            _attributes.CombineAttribute(name, value);
            return this;
        }

        public override string ToString()
        {
            StringBuilder tag = new StringBuilder();

            if (!string.IsNullOrEmpty(this._tagName))
                tag.AppendFormat("<{0}", _tagName);

            if (_attributes.Count > 0)
            {
                var array = _attributes.Select(kvp =>
                string.Format("{0}='{1}'", kvp.Key, kvp.Value))
                    .ToArray();
                tag.Append(" ");
                tag.Append(string.Join(" ", array));
            }

            if (_body.Length > 0 || _tagName == "i" || _tagName == "div")
            {
                if (!string.IsNullOrEmpty(this._tagName) || this._attributes.Count > 0)
                    tag.Append(">");
                tag.Append(_body.ToString());

                if (!string.IsNullOrEmpty(this._tagName))
                    tag.AppendFormat("</{0}>", this._tagName);
            }
            else
                if (!string.IsNullOrEmpty(this._tagName))
                tag.Append("/>");

            return tag.ToString();
        }

        public static implicit operator string (FluentTagBuilder builder)
            => builder.ToString();
    }
}
