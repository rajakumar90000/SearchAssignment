using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace SearchEngine
{
    public class YandexSearch : ISearchOp
    {
        public string UriString
        {
            get;
            set;
        }
        public string BuildSearchUrl(string SearchWord)
        {
            string key = "8de5aa84-8e7e-490d-b785-e4de0e8cfb75";

            string lang = "en-US";

            string url = @"https://search-maps.yandex.ru/v1/?text={0}&lang={1}&apikey={2}";

            UriString =string.Format(url, SearchWord,lang,key);

            return UriString;

        }
        public async Task<string> GetSearchResult(string SearcUrhWords)
        {
           
            HttpClient client = new HttpClient();          

            var httpResponseMessage =  await client.GetAsync(UriString);
            var responseContent = await  httpResponseMessage.Content.ReadAsStringAsync();
            var jobject =  (JObject)JsonConvert.DeserializeObject(responseContent);

            if (jobject == null ||jobject["properties"] == null||
                jobject["properties"]["SearchRequest"] == null ||
                jobject["properties"]["ResponseMetaData"]["SearchRequest"] == null||
             jobject["properties"]["ResponseMetaData"]["SearchRequest"]["results"] == null)
            {
                return "No Records";
            }
            else
            {
                var totalResults = (JValue)jobject["properties"]["ResponseMetaData"]["SearchRequest"]["results"];

                return totalResults != null ? totalResults.Value<string>() : string.Empty;
            }

//return totalResults; 

        }
    }
}




