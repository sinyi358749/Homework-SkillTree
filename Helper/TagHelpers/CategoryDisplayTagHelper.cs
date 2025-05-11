using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Homework_SkillTree.TagHelpers
{
    public class CategoryDisplayTagHelper : TagHelper
    {
        public string Category { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";
            output.Attributes.SetAttribute("class", Category == "¤ä¥X" ? "text-danger" : "text-primary");

            var label = new TagBuilder("label");
            label.InnerHtml.Append(Category);

            output.Content.SetHtmlContent(label);
        }
    }
}
