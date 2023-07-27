using System.Globalization;

namespace ESTA.Helpers
{
    public class CultureManager
    {

        public static IEnumerable<string> GetCountries()
        {
          

            SortedSet<string> listOfCountries = new();

            foreach (var culturesInfo in CultureInfo.GetCultures(CultureTypes.AllCultures))
            {
                try
                {
                    listOfCountries.Add(new RegionInfo(culturesInfo.LCID).EnglishName);
                }
                catch (Exception)
                {
                    continue;
                }
            }

            return listOfCountries;
        }
    }
}
