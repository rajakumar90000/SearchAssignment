using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SearchEngine
{
    public class GoogleSearch : ISearchOp
    {
        //
        // PROPERTIES
        //
        public string Key
        {
            get;
            set;
        }
        public string CX
        {
            get;
            set;
        }
        public int Num
        {
            get;
            set;
        }
        public int Start
        {
            get;
            set;
        }
        public Helper.SafeLevel SafeLevel
        {
            get;
            set;
        }

        public string UriString
        {
            get;
            set;
        }


        public GoogleSearch()
        {
            this.Num = 10;
            this.Start = 1;
            this.SafeLevel = Helper.SafeLevel.off;
            this.Key = "AIzaSyANW3zQR8uE3HSpa9wSyjeY52AzlgJjCJY";
            this.CX = "017576662512468239146:omuauf_lfve";
        }

        public  string BuildSearchUrl(string search)
        {
            // Check Parameters
            if (string.IsNullOrWhiteSpace(this.Key))
            {
                throw new Exception("Google Search 'Key' cannot be null");
            }
            if (string.IsNullOrWhiteSpace(this.CX))
            {
                throw new Exception("Google Search 'CX' cannot be null");
            }
            if (string.IsNullOrWhiteSpace(search))
            {
                throw new ArgumentNullException("search");
            }
            if (this.Num < 0 || this.Num > 10)
            {
                throw new ArgumentNullException("Num must be between 1 and 10");
            }
            if (this.Start < 1 || this.Start > 100)
            {
                throw new ArgumentNullException("Start must be between 1 and 100");
            }

            // Build Query
            string query = string.Empty;

            query += string.Format("key={0}", this.Key);
            query += string.Format("&cx={0}", this.CX);
            query += string.Format("&q={0}", search);
            query += string.Format("&num={0}", this.Num);
            query += string.Format("&start={0}", this.Start);


            // Construct URL
            UriBuilder builder = new UriBuilder()
            {
                Scheme = Uri.UriSchemeHttps,
                Host = "www.googleapis.com",
                Path = "customsearch/v1",
                Query = query
            };

            UriString = builder.Uri.ToString();

            return UriString;
        }

        public async Task<string> GetSearchResult(string SearchWord)
        {          

            // Submit Request
            HttpClient client = new HttpClient();

            var httpResponseMessage = await client.GetAsync(UriString);

            var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();

            var jobject = (JObject)JsonConvert.DeserializeObject(responseContent);

            if (jobject == null || jobject["searchInformation"] == null ||
             jobject["searchInformation"]["totalResults"] == null)
            {
                return "No Records";
            }
            else
            {
                var totalResults = (JValue)jobject["searchInformation"]["totalResults"];

                return totalResults!=null ? totalResults.Value<string>() : string.Empty ;
            }
        }
    }
}