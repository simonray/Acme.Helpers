using Acme.Helpers.Demo.Internal;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using System;

namespace Acme.Helpers.TagHelpers
{
    public enum shStyle { Flat, Plastic, FlatSquare, }
    public enum shColor { BrightGreen, Green, YellowGreen, Yellow, Orange, Red, LightGrey, Blue, }
    public enum shImage { Png, Svg, }
    public enum shType
    {
        BowerVersion, NodeVersion, GithubTag, GithubRelease, NugetVersion, NugetPreRelease,
        GithubIssues, GithubForks, GithubStars, GithubFollowers,
    };

    [TargetElement(TagName.Shield, Attributes = "subject,type")]
    [TargetElement(TagName.Shield, Attributes = "subject,status")]
    public class ShieldTagHelper : TagHelper
    {
        public string Subject { get; set; }
        public shType? Type { get; set; }

        public string Status { get; set; }
        public shColor Color { get; set; } = shColor.Blue;

        public shStyle Style { get; set; } = shStyle.Flat;
        public shImage Image { get; set; } = shImage.Png;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (Type != null && Status != null)
                throw new ArgumentException($"'{nameof(Type)}' and '{nameof(Status)}' attributes cannot be used together.");
            output.TagName = null;
            output.Content.SetContent(ShieldGenerator.GenerateShieldMarkup(Subject, Type, Status, Color, Style, Image));
        }
    }
}
