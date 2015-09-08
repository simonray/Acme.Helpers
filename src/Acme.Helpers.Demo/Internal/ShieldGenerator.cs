using Acme.Helpers.Core.Extensions;
using Acme.Helpers.Core.Library;
using Acme.Helpers.TagHelpers;
using System;
using System.Collections.Generic;

namespace Acme.Helpers.Demo.Internal
{
    /// <exclude />
    internal static class ShieldGenerator
    {
        /// <exclude />
        private static IDictionary<shType, string> GithubForkmeMask
        {
            get
            {
                return new Dictionary<shType, string>()
                {
                    { shType.BowerVersion, "https://img.shields.io/bower/v/{subject}.svg" },
                    { shType.NodeVersion, "https://img.shields.io/node/v/{subject}.svg" },
                    { shType.GithubTag, "https://img.shields.io/github/tag/{subject}.{image}" },
                    { shType.GithubRelease, "https://img.shields.io/github/release/{subject}.{image}" },
                    { shType.NugetVersion, "https://img.shields.io/nuget/v/{subject}.{image}" },
                    { shType.NugetPreRelease, "https://img.shields.io/nuget/vpre/{subject}.{image}" },
                    { shType.GithubIssues, "https://img.shields.io/github/issues/{subject}.{image}" },
                    { shType.GithubForks, "https://img.shields.io/github/forks/{subject}.{image}" },
                    { shType.GithubStars, "https://img.shields.io/github/stars/{subject}.{image}" },
                    { shType.GithubFollowers, "https://img.shields.io/github/stars/{subject}.{image}" },
                };
            }
        }

        /// <exclude />
        public static FluentTagBuilder GenerateShieldMarkup(string subject, shType? type = null, string status = null, shColor color = shColor.Green, shStyle style = shStyle.Flat, shImage image = shImage.Png)
        {
            string url;
            subject = subject.Split(' ')[0];
            if (type == null)
                url = Substitute("https://img.shields.io/badge/{subject}-{status}-{color}.{image}", subject.Replace("-", ""), status, color, image, style);
            else
                url = Substitute(GithubForkmeMask[(shType)type], subject, status, color, image, style);

            return new FluentTagBuilder().Tag("img", null, new { src = url });
        }

        /// <exclude />
        private static string Substitute(string url, string subject, string status, shColor? color, shImage? image, shStyle? style)
        {
            var newUrl = url
                .Replace("{subject}", subject as string)
                .Replace("{status}", status as string)
                .Replace("{color}", color.ToStr())
                .Replace("{image}", image.ToStr());
            if (style != shStyle.Flat)
                newUrl += string.Format("?style={0}", style.ToStr());
            return newUrl;
        }

        /// <exclude />
        private static string ToStr(this Enum e)
        {
            if (e == null) return string.Empty;
            return Enum.GetName(e.GetType(), e).SplitCamelCaseLower();
        }
    }
}
