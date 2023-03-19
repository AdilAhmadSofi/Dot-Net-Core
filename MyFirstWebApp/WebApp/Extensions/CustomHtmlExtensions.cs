using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp
{
    public static class CustomHTMLExtensions
    {
        /// <summary>
        /// Image is used to create an Image Tag
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="src"></param>
        /// <param name="alt"></param>
        /// A new Microsoft.AspNetCore.Html.IHtmlContent containing the <img> element
        public static IHtmlContent Image(this IHtmlHelper helper, string src, string alt)
        {
            return new HtmlString($"<img src='{src}' alt='{alt}'");
        }
    }
}
