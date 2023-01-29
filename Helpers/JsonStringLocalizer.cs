using Microsoft.Extensions.Localization;
using Newtonsoft.Json;

namespace ESTA.Helpers
{
    public class JsonStringLocalizer : IStringLocalizer
    {
        public LocalizedString this[string name]
        {
            get
            {
                var value = GetString(name);
                return new LocalizedString(name, value);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {

            get
            {
                var actualValue=this[name];

                return actualValue.ResourceNotFound ?
                     actualValue
                     :
                     new LocalizedString(name, String.Format(actualValue.Value, arguments));





            }

        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {

            var filepath = $"Resources/{Thread.CurrentThread.CurrentCulture}.json";
            using FileStream fileStream =
                          new(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using StreamReader streamReader = new(fileStream);
            using JsonTextReader jsonTextReader = new(streamReader);

            while (jsonTextReader.Read())
            {
                if (jsonTextReader.TokenType != JsonToken.PropertyName) continue;
               

                var key = jsonTextReader.Value as string;
                jsonTextReader.Read();
                var value = new JsonSerializer().Deserialize<string>(jsonTextReader);


                 yield   return new LocalizedString(key, value);
                
            }


        }

        private string GetString(string key)
        {
            var filepath = $"Resources/{Thread.CurrentThread.CurrentCulture}.json";
            var fullpath = Path.GetFullPath(filepath);

            return GetValueFromJson(fullpath, key);
        }

        private string GetValueFromJson(string filepath, string property)
        {
            using FileStream fileStream =
                new(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using StreamReader streamReader = new(fileStream);
            using JsonTextReader jsonTextReader = new(streamReader);

            while (jsonTextReader.Read())
            {
                if (
                    jsonTextReader.TokenType == JsonToken.PropertyName
                    && jsonTextReader.Value as string == property
                )
                {
                    jsonTextReader.Read();
                    return new JsonSerializer().Deserialize<string>(jsonTextReader);
                }
            }

            return string.Empty;
        }
    }
}
