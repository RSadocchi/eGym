using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGym.MVC.Helpers
{
    public class ArticleTagHelper : TagHelper
    {
        public string Id { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            Process(context, output);
            await Task.CompletedTask;
        }
    }
}
