// We use the HttpUtility class from the System.Web namespace  
using System.Web;
using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json.Linq;
using SearchEngine;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
public class DuckDuckgoSearch : ISearchOp
{
	public string UriString
	{
		get;
		set;
	}

	public string BuildSearchUrl(string SearchWord)
	{
		string	url = "https://api.duckduckgo.com/?q={0}&format=json&pretty=1";
	
		UriString = string.Format(url, SearchWord);

		return UriString;
	}
	
	public async Task<string> GetSearchResult(string SearchWords)
	{
		
		HttpClient client = new HttpClient();

		var httpResponseMessage = await client.GetAsync(UriString);
		var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
		var jobject = (JObject)JsonConvert.DeserializeObject(responseContent);

		if (jobject == null || jobject["RelatedTopics"]== null)
		{
			return "0";
		}
		else
		{
			return jobject["RelatedTopics"].ToList().Count().ToString();
		}
		
	}

}