using System.Text.RegularExpressions;

namespace ESTA.Helpers
{
    public static class HtmlHelper
    {
        public static string RemoveHTMLTags(string text, int length = 100)
        {
            string Desc = Regex.Replace(text.Trim(), "<.*?>", String.Empty);
            if (Desc.Length > length)
                return Desc.Substring(0, length);
            else
                return Desc;
        }
    }
}
