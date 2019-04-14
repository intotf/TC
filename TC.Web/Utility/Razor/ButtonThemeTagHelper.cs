using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TC.Web.Utility.Razor
{
    public class ButtonThemeTagHelper : TagHelper
    {

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context.Items.ContainsKey("theme"))
                output.Attributes.SetAttribute("class",
                    $"btn btn-{context.Items["theme"]}");
            base.Process(context, output);
        }

    }
}
