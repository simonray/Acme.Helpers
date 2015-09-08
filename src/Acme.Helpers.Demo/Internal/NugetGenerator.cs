using Acme.Helpers.Core.Library;
using Acme.Helpers.TagHelpers;
using System.Collections.Generic;
using System.Linq;

namespace Acme.Helpers.Demo.Internal
{
    /// <exclude />
    internal static class NugetGenerator
    {
        /// <exclude />
        private static string NugetCommandStyles
        {
            get
            {
                var styles = new Dictionary<string, string>()
                {
                    { "-moz-border-radius", "5px"},
                    { "-webkit-border-radius", "5px"},
                    { "background-color", "#202020"},
                    { "border", "4px solid silver"},
                    { "border-radius", "5px"},
                    { "box-shadow", "2px 2px 3px #6e6e6e"},
                    { "color", "#e2e2e2"},
                    { "display", "block"},
                    { "font", "1.4em \"andale mono\",\"lucida console\",monospace"},
                    { "line-height", "1.4em"},
                    { "overflow", "auto"},
                    { "padding", "15px"},
                };
                return string.Join("; ", styles.Select(kv => string.Format("{0}: {1}", kv.Key.ToString(), kv.Value.ToString())));
            }
        }

        /// <exclude />
        private static string NugetPanelStyles
        {
            get
            {
                var styles = new Dictionary<string, string>()
                {
                    { "padding", "0px 19px 19px 19px"},
                };
                return string.Join("; ", styles.Select(kv => string.Format("{0}: {1}", kv.Key.ToString(), kv.Value.ToString())));
            }
        }

        /// <exclude />
        public static FluentTagBuilder GenerateNugetCommand(string package, bool ignoreMessage = false, string message = null)
        {
            string styles = NugetCommandStyles;
            return new FluentTagBuilder()
                .Action(p => {
                    if (!ignoreMessage)
                    {
                        if (!string.IsNullOrEmpty(message))
                            p.StartTag("p")
                                .Append(message)
                            .EndTag();
                        else
                            p.StartTag("p")
                                .Append("To install, run the following command in the ")
                                .StartTag("a")
                                    .Attribute("href", "http://docs.nuget.org/docs/start-here/using-the-package-manager-console")
                                    .Append("Package Manager Console.")
                                .EndTag()
                            .EndTag();
                    }
                })
                .StartTag("code")
                    .Attribute("style", NugetCommandStyles)
                    .Append($"PM> Install-Package {package}")
                .EndTag();
        }

        /// <exclude />
        public static FluentTagBuilder GenerateNugetPanel(string package, string description, string repository = null)
        {
            return new FluentTagBuilder()
                .StartTag("div", "well").Style(NugetPanelStyles)
                    .StartTag("div", "span8")
                        .StartTag("h1")
                            .Append($"{package} ")
                            .Append(ShieldGenerator.GenerateShieldMarkup(package, shType.NugetVersion))
                        .EndTag()
                        .Tag("hr")
                        .Tag("p", description)
                        .Append(GenerateNugetCommand(package))
                        .ActionIf(repository != null, tag =>
                        {
                            tag.Tag("hr");
                            tag.Append(GenerateDownloadButton(repository, "Download", "fa fa-cloud-download", $"http://github.com/{repository}/zipball/master"));
                            tag.Append(" ");
                            tag.Append(GenerateDownloadButton(repository, "Source", "fa fa-github", $"http://github.com/{repository}"));
                        })
                    .EndTag()
                .EndTag();
        }

        /// <exclude />
        private static FluentTagBuilder GenerateDownloadButton(string package, string text, string icon, string url)
        {
            return new FluentTagBuilder()
                .StartTag("a", "btn btn-lg btn-info btn-md")
                    .Attribute("href", $"{url}")
                    .Attribute("style", "vertical-align: middle;")
                    .Tag("i", null, new { @class = icon })
                    .Append($" {text}")
                .EndTag();
        }
    }
}
