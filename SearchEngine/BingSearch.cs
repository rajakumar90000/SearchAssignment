using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Search.WebSearch;
using Microsoft.Azure.CognitiveServices.Search.WebSearch.Models;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace SearchEngine
{
	public class BingSearch : ISearchOp
	{

		public string UriString
		{
			get;
			set;
		}

		public string customConfigId
		{
			get;
			set;
		}

		public string mkt
		{
			get;
			set;
		}

		public string Url
		{
			get;
			set;
		}

		public BingSearch()
		{

		}
		public string BuildSearchUrl(string SearchWord)
		{

			var customConfigId = "8817cfc9-734d-4aa2-b460-f0a7029ccc20";

			var mkt = "en-US";

			var url = "https://api.cognitive.microsoft.com/bingcustomsearch/v7.0/search?";

			string query = string.Empty;
			query = string.Format("{0}", url);
			query += string.Format("q={0}", SearchWord);
			query += string.Format("&customconfig={0}", customConfigId);
			query += string.Format("&mkt={0}", mkt);

			UriString = query;

			return string.Empty;
		}
		//
		public async Task<string> GetSearchResult(string SearchWords)
		{

			try
			{

				var subscriptionKey = "a4437a09efc1424a9043165bcd93a7b8";

				HttpClient client = new HttpClient();
				client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

				var httpResponseMessage = await client.GetAsync(UriString);
				var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();

				var jobject = (JObject)JsonConvert.DeserializeObject(responseContent);

				if (jobject == null || jobject["webPages"] == null || jobject["webPages"]["totalEstimatedMatches"] == null)
				{
					return "No Records";
				}
				else
				{
					var totalResults = (JValue)jobject["webPages"]["totalEstimatedMatches"];

					return totalResults != null ? totalResults.Value<string>() : string.Empty;
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}
	}
}