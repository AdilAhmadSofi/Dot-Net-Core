using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebApp
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("email")]
    public class EmailTagHelper : TagHelper
    {
        public string? href { get; set; }
        public string? CssClass { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.Attributes.Add("href", $"mailto:{href}");
            output.Content.SetContent("Email Me");
        }
    }
}
