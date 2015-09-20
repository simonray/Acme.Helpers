using Acme.Helpers.Core.Extensions;
using Acme.Helpers.Core.Library;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acme.Helpers.TagHelpers
{
    [TargetElement("directory")]
    public class DirectoryTagHelper : BasePagerTagHelper, ISupportAnchor, ISupportDirectory
    {
        #region ISupportDirectory
        ///<inheritDoc/>
        [HtmlAttributeName("alphabet")]
        public string DirectoryAlphabet { get; set; } = DirectoryDefaults.Alphabet;
        ///<inheritDoc/>
        [HtmlAttributeName("class")]
        public string DirectoryClass { get; set; } = DirectoryDefaults.DefaultClass;
        ///<inheritDoc/>
        [HtmlAttributeName("numbers")]
        public string DirectoryNumbers { get; set; } = DirectoryDefaults.Numbers;
        ///<inheritDoc/>
        [HtmlAttributeName("param")]
        public string DirectoryParam { get; set; } = DirectoryDefaults.DefaultParameter;
        ///<inheritDoc/>
        [HtmlAttributeName("show-active")]
        public bool DirectoryShowActive { get; set; } = DirectoryDefaults.ShowActive;
        ///<inheritDoc/>
        [HtmlAttributeName("start-at")]
        public string DirectoryStartAt { get; set; } = null;
        ///<inheritDoc/>
        [HtmlAttributeName("display-mode")]
        public DirectoryDisplayMode DirectoryDisplayMode { get; set; } = DirectoryDefaults.Mode;
        #endregion

        private char Current
        {
            get
            {
                return ViewContext.HttpContext.Request.Query.ContainsKey(DirectoryParam) ?
                    ViewContext.HttpContext.Request.Query[DirectoryParam][0] : StartAt;
            }
        }

        private char StartAt
        {
            get { return DirectoryStartAt?[0] ?? (StartsWithNumbers ? DirectoryDefaults.Numbers[0] : DirectoryDefaults.Alphabet[0]); }
        }

        private bool StartsWithNumbers
        {
            get { return (DirectoryDisplayMode == DirectoryDisplayMode.Numbers || DirectoryDisplayMode == DirectoryDisplayMode.NumbersLetters); }
        }

        public DirectoryTagHelper(IUrlHelper urlHelper)
        {
            UrlHelper = urlHelper;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.ProcessAsync(context, output);

            output.TagName = null;
            output.Content.SetContent(Create());
            var content = (await context.GetChildContentAsync()).ToString()
                .ReplaceStringTokens(new Dictionary<string, object> {["Current"] = Current, });
            if (!string.IsNullOrEmpty(content))
                output.PostContent.SetContent(content);
        }

        private string Create()
        {
            return new FluentTagBuilder()
                .StartTag("ul", DirectoryClass).Style("text-align: center;")
                    .Action(tag =>
                    {
                        switch (DirectoryDisplayMode)
                        {
                            case DirectoryDisplayMode.Letters:
                                tag.Append(ProcessLetters());
                                break;
                            case DirectoryDisplayMode.LettersNumbers:
                                tag.Append(ProcessLetters());
                                tag.Append(ProcessNumbers());
                                break;
                            case DirectoryDisplayMode.Numbers:
                                tag.Append(ProcessNumbers());
                                break;
                            case DirectoryDisplayMode.NumbersLetters:
                                tag.Append(ProcessNumbers());
                                tag.Append(ProcessLetters());
                                break;
                        }
                    })
                .EndTag();
        }

        private FluentTagBuilder ProcessLetters()
        {
            FluentTagBuilder tag = new FluentTagBuilder();
            foreach (var letter in DirectoryAlphabet.ToCharArray())
            {
                tag.Append(CreateListItem(letter));
            }
            return tag;
        }

        private FluentTagBuilder ProcessNumbers()
        {
            FluentTagBuilder tag = new FluentTagBuilder();
            foreach (var number in DirectoryNumbers.ToCharArray())
            {
                tag.Append(CreateListItem(number));
            }
            return tag;
        }

        private FluentTagBuilder CreateListItem(char ch)
        {
            RouteValues[DirectoryParam] = ch;
            return new FluentTagBuilder()
                .StartTag("li")
                    .AttributeIf(DirectoryShowActive && (Current.Equals(ch)), "class", "active")
                    .Anchor(UrlHelper.Action(AspAction, AspController, RouteValues, AspProtocol, AspHost, AspFragment), ch.ToString())
                .EndTag();
        }
    }
}
