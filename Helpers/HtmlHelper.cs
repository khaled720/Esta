using System.Text.RegularExpressions;

namespace ESTA.Helpers
{
    public static class HtmlHelper
    {
        public static string RemoveHTMLTags(string text, int length = 100)
        {
            string Desc = Regex.Replace(text.Trim(), "<.*?>", String.Empty);
            if (Desc.Length > length)
            {
                string StrRes = Desc[..length];
                int lastSpaceIndex = StrRes.LastIndexOf(' ');
                return StrRes[..lastSpaceIndex];
            }
            else
                return Desc;
        }
    }
}
