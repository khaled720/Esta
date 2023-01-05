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
            //else if (Desc.Length > 50)
            //    return Desc.Substring(0, 50);
            //else if (Desc.Length > 20)
            //    return Desc.Substring(0, 20);
            else
                return Desc;
        }
    }
}
