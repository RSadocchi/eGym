using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGym.MVC.Helpers
{
    public enum ModalSixeEnum { xs, sm, md, lg, xl }

    public class ModalContainerTagHelper : TagHelper
    {
        public string ID { get; set; }
        public ModalSixeEnum Size { get; set; } = ModalSixeEnum.lg;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "modal fade");
            output.Attributes.SetAttribute("id", ID ?? System.Guid.NewGuid().ToString());
            output.Attributes.SetAttribute("tabindex", "-1");
            output.Attributes.SetAttribute("role", "dialog");
            output.Attributes.SetAttribute("aria-hidden", "true");
            var sb = new StringBuilder();
            sb.AppendLine($@"<div class='modal-dialog modal-dialog-centered modal-{Size.ToString()}' role='document'>");
            sb.AppendLine($@"<div class='modal-content'>");
            output.PreContent.SetHtmlContent(sb.ToString());
            output.Content.SetHtmlContent(output.GetChildContentAsync().Result.GetContent());
            output.PostContent.SetHtmlContent("</div></div>");
            output.TagMode = TagMode.StartTagAndEndTag;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            Process(context, output);
            await Task.CompletedTask;
        }
    }

    public class ModalHeaderTagHelper : TagHelper
    {
        public string ID { get; set; }
        public string Title { get; set; }
        /// <summary>
        /// Default "text-uppercase", "text_black", "f-size-200", "f-wght-500", "p-0"
        /// </summary>
        public List<string> CssTitle { get; set; } = new List<string>() { "text-uppercase", "text_black", "f-size-150", "f-wght-600", "p-0" };

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "modal-header");
            var sb = new StringBuilder();
            sb.AppendLine($@"<div {(!string.IsNullOrWhiteSpace(ID) ? $" id='{ID}' " : "")} class='{string.Join(" ", CssTitle)}'>{Title}</div>");
            sb.AppendLine($@"<span class='modal-closer' data-dismiss='modal'><i class='fal fa-times'></i></span>");
            output.PreContent.SetHtmlContent(sb.ToString());
            output.TagMode = TagMode.StartTagAndEndTag;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            Process(context, output);
            await Task.CompletedTask;
        }
    }

    public class ModalBodyTagHelper : TagHelper
    {
        public string ID { get; set; }
        /// <summary>
        /// Default "modal-body", "modal-body-scrollable"
        /// </summary>
        public List<string> CssBody { get; set; } = new List<string>() { "modal-body", "modal-body-scrollable" };

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", $"{string.Join(" ", CssBody)}");
            if (!string.IsNullOrWhiteSpace(ID)) output.Attributes.SetAttribute("id", ID);
            var sb = new StringBuilder();
            output.PreContent.SetHtmlContent(sb.ToString());
            output.Content.SetHtmlContent(output.GetChildContentAsync().Result.GetContent());
            output.TagMode = TagMode.StartTagAndEndTag;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            Process(context, output);
            await Task.CompletedTask;
        }
    }

    public class ModalFooterTagHelper : TagHelper
    {
        public string ID { get; set; }
        /// <summary>
        /// Default "modal-footer"
        /// </summary>
        public List<string> CssBody { get; set; } = new List<string>() { "modal-footer" };

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", $"{string.Join(" ", CssBody)}");
            if (!string.IsNullOrWhiteSpace(ID)) output.Attributes.SetAttribute("id", ID);
            var sb = new StringBuilder();
            output.PreContent.SetHtmlContent(sb.ToString());
            output.Content.SetHtmlContent(output.GetChildContentAsync().Result.GetContent());
            output.TagMode = TagMode.StartTagAndEndTag;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            Process(context, output);
            await Task.CompletedTask;
        }
    }
}
