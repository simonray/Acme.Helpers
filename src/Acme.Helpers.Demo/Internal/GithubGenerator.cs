using Acme.Helpers.Core.Library;
using Acme.Helpers.TagHelpers;
using System.Collections.Generic;

namespace Acme.Helpers.Demo.Internal
{
    /// <exclude />
    internal static class GithubGenerator
    {
        /// <exclude />
        private static IDictionary<GithubForkmeColor, string> GithubForkmeImages
        {
            get
            {
                return new Dictionary<GithubForkmeColor, string>()
                {
                    { GithubForkmeColor.Red, "aa0000"},
                    { GithubForkmeColor.Green, "007200"},
                    { GithubForkmeColor.DarkBlue, "121621"},
                    { GithubForkmeColor.Orange, "ff7600"},
                    { GithubForkmeColor.Gray, "6d6d6d"},
                    { GithubForkmeColor.White, "ffffff"},
                };
            }
        }

        /// <exclude />
        public static FluentTagBuilder GenerateGithubForkme(string repository, GithubForkmeSide side, GithubForkmeColor color)
        {
            return new FluentTagBuilder()
                .StartTag("a").Attribute("href", $"https://github.com/{repository}")
                    .StartTag("img")
                        .Action(tag =>
                        {
                            if (side.Equals(GithubForkmeSide.Left))
                                tag.Style("position: absolute; top: 0; left: 0; border: 0; z - index:999999");
                            else
                                tag.Style("position: absolute; top: 0; right: 0; border: 0; z-index:999999");
                        })
                        .Attribute("src", $"https://s3.amazonaws.com/github/ribbons/forkme_{side.ToString().ToLower()}_{color.ToString().ToLower()}_{GithubForkmeImages[color]}.png")
                        .Attribute("alt", "Fork me on GitHub")
                    .EndTag()
                .EndTag();
        }
    }
}
