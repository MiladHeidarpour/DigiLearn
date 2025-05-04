using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DigiLearn.Web.TagHelpers;

public class OpenModal : TagHelper
{
    public string Url { get; set; }
    public string ModalTitle { get; set; } = "";
    public string Class { get; set; } = "btn btn-success";
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "button";
        output.Attributes.Add("class", Class);
        output.Attributes.Add("OnClick", $"OpenModal('{Url}','defaultModal','{ModalTitle}')");

        base.Process(context, output);
    }
}