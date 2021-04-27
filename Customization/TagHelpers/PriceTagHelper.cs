using ElearningDemo.Models.ValueType;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ElearningDemo.Customization.TagHelpers
{
    public class PriceTagHelper : TagHelper
    {
        public Money FullPrice { get; set; }
        public Money CurrentPrice { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";
            output.Content.AppendHtml($"{CurrentPrice}");
            if (!FullPrice.Equals(CurrentPrice))
            {
                output.Content.AppendHtml($"<br/><s>{FullPrice}</s>");
            }
        }
    }
}