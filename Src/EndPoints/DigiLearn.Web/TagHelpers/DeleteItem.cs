using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DigiLearn.Web.TagHelpers;

public class DeleteItem : TagHelper
{
    public string Url { get; set; }
    public string Description { get; set; }
    public string Class { get; set; } = "btn btn-danger btn-sm";
    public bool IsButtonTag { get; set; } = true;
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = IsButtonTag is true ? "button" : "a";
        output.Attributes.Add("onClick", $"deleteItem('{Url}','{Description}')");

        //output.AddClass("btn",HtmlEncoder.Default);
        //output.AddClass("btn-danger",HtmlEncoder.Default);
        //output.AddClass("btn-danger",HtmlEncoder.Default);

        //output.Attributes.Add("Class", "btn btn-danger btn-sm");

        output.Attributes.Add("class", Class);

        base.Process(context, output);
    }
}