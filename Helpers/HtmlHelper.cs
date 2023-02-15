using System.Text.RegularExpressions;

namespace ESTA.Helpers
{
    public static class HtmlHelper
    {
        public static string RemoveHTMLTags(string text)
        {
            string Desc = Regex.Replace(text.Trim(), "<.*?>", String.Empty);
            if (Desc.Length > 100)
                return Desc.Substring(0, 100);
            else
                return Desc;
        }
    }
}
