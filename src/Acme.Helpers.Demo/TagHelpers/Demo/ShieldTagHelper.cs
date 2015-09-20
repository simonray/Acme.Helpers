using Acme.Helpers.Demo.Internal;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using System;

namespace Acme.Helpers.TagHelpers
{
    [TargetElement(TagName.Shield, Attributes = "subject,type")]
    [TargetElement(TagName.Shield, Attributes = "subject,status")]
    public class ShieldTagHelper : TagHelper
    {
        public string Subject { get; set; }
        public ShieldType? Type { get; set; }

        public string Status { get; set; }
        public ShieldColor Color { get; set; } = ShieldColor.Blue;

        public ShieldStyle Style { get; set; } = ShieldStyle.Flat;
        public ShieldImage Image { get; set; } = ShieldImage.Png;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (Type != null && Status != null)
                throw new ArgumentException($"'{nameof(Type)}' and '{nameof(Status)}' attributes cannot be used together.");
            output.TagName = null;
            output.Content.SetContent(ShieldGenerator.GenerateShieldMarkup(Subject, Type, Status, Color, Style, Image));
        }
    }
}
