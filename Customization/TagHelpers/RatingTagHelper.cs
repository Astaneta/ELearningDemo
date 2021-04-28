using System;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ElearningDemo.Customization.TagHelpers
{
    public class RatingTagHelper : TagHelper
    {
        public double Value { get; set; } // Il valore assunto dall'attributo value nel taghelper viene bindato qui (stesso nome)
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            for(int i = 1; i <= 5; i++)
            {
                //double value = Convert.ToDouble(context.AllAttributes["value"].Value);

                if (Value >= i - 0.2)
                {
                    output.Content.AppendHtml("<i class=\"fas fa-star\"></i>");
                }
                else if (Value > i - 0.5)
                {
                    output.Content.AppendHtml("<i class=\"fas fas fa-star-half-alt\"></i>");
                }
                else
                {
                    output.Content.AppendHtml("<i class=\"far fa-star\"></i>");
                }
            }
        }
    }
}