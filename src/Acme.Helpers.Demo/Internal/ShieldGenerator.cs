using Acme.Helpers.Core.Extensions;
using Acme.Helpers.Core.Library;
using System;
using System.Collections.Generic;

namespace Acme.Helpers.Demo.Internal
{
    /// <exclude />
    internal static class ShieldGenerator
    {
        /// <exclude />
        private static IDictionary<ShieldType, string> GithubForkmeMask
        {
            get
            {
                return new Dictionary<ShieldType, string>()
                {
                    { ShieldType.BowerVersion, "https://img.shields.io/bower/v/{subject}.svg" },
                    { ShieldType.NodeVersion, "https://img.shields.io/node/v/{subject}.svg" },
                    { ShieldType.GithubTag, "https://img.shields.io/github/tag/{subject}.{image}" },
                    { ShieldType.GithubRelease, "https://img.shields.io/github/release/{subject}.{image}" },
                    { ShieldType.NugetVersion, "https://img.shields.io/nuget/v/{subject}.{image}" },
                    { ShieldType.NugetPreRelease, "https://img.shields.io/nuget/vpre/{subject}.{image}" },
                    { ShieldType.GithubIssues, "https://img.shields.io/github/issues/{subject}.{image}" },
                    { ShieldType.GithubForks, "https://img.shields.io/github/forks/{subject}.{image}" },
                    { ShieldType.GithubStars, "https://img.shields.io/github/stars/{subject}.{image}" },
                    { ShieldType.GithubFollowers, "https://img.shields.io/github/stars/{subject}.{image}" },
                };
            }
        }

        /// <exclude />
        public static FluentTagBuilder GenerateShieldMarkup(string subject, ShieldType? type = null, string status = null, ShieldColor color = ShieldColor.Green, ShieldStyle style = ShieldStyle.Flat, ShieldImage image = ShieldImage.Png)
        {
            string url;
            subject = subject.Split(' ')[0];
            if (type == null)
                url = Substitute("https://img.shields.io/badge/{subject}-{status}-{color}.{image}", subject.Replace("-", ""), status, color, image, style);
            else
                url = Substitute(GithubForkmeMask[(ShieldType)type], subject, status, color, image, style);

            return new FluentTagBuilder().Tag("img", null, new { src = url });
        }

        /// <exclude />
        private static string Substitute(string url, string subject, string status, ShieldColor? color, ShieldImage? image, ShieldStyle? style)
        {
            var newUrl = url
                .Replace("{subject}", subject as string)
                .Replace("{status}", status as string)
                .Replace("{color}", color.ToStr())
                .Replace("{image}", image.ToStr());
            if (style != ShieldStyle.Flat)
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
