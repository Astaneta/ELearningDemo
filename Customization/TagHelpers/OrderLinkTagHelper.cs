using ELearningDemo.Models.InputModels;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ElearningDemo.Customization.TagHelpers
{
    public class OrderLinkTagHelper : AnchorTagHelper
    {
        public string OrderBy { get; set; }
        public CoursesListInputModel Input { get; set; }

        public OrderLinkTagHelper(IHtmlGenerator generator) : base(generator)
        {

        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";

            //Imposto i router per il taghelper
            RouteValues["search"] = Input.Search;
            RouteValues["orderby"] = OrderBy;
            RouteValues["ascending"] = (Input.OrderBy == OrderBy ? !Input.Ascending : Input.Ascending).ToString();

            //Questo methodo genera l'output
            base.Process(context, output);

            //Indicatore di direzione
            if (Input.OrderBy == OrderBy)
            {
                var direction = Input.Ascending ? "up" : "down";
                output.PostContent.SetHtmlContent($" <i class=\"fas fa-caret-{direction}\"></i>");
            }
        }
    }
}