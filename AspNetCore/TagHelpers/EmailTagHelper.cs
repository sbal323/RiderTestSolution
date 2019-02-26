using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AspNetCore.TagHelpers
{
    [HtmlTargetElement("email", Attributes = "to, subject")]
    public class EmailTagHelper:ITagHelper
    {
        public void Init(TagHelperContext context)
        {
            //throw new System.NotImplementedException();
        }

        public async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var body = (await output.GetChildContentAsync()).GetContent();
            output.TagName = "a";
            var to = context.AllAttributes["to"].Value.ToString();
            var subject = context.AllAttributes["subject"].Value.ToString();
            var mailto = $"mailto:{to}";
            if (!string.IsNullOrWhiteSpace(subject))
            {
                mailto = $"{mailto}?subject={subject}&body={body}";
            }
            output.Attributes.Clear();
            output.Attributes.Add("href",mailto);
            output.Content.Clear();
            output.Content.AppendFormat("Email {0}", to);
        }

        public int Order { get; }
    }
}