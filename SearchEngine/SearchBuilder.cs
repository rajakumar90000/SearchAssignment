using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngine
{
	public sealed class SearchBuilder
	{

		private readonly SearchFactory _factory = null;
		public SearchBuilder(SearchFactory factory)
		{
			_factory = factory;
		}
		public void GenerateSearchResult(string[] inputs)
		{
			try
			{
				var searchList = new List<string>();

				for (int i = 0; i <= inputs.Length - 1; i++) // Loop through array
				{
					searchList.Add(inputs[i]);
				}

				var result = BuildSearchResult(searchList).GetAwaiter().GetResult();

				PrintSearchResult(result);

				Console.ReadLine();
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception :" + ex.Message);
			}
			finally { }
		}

		public async Task<IEnumerable<SearchResult>> BuildSearchResult(dynamic searchList)
		{
			var allResults = new List<SearchResult>();

			SearchFactory factory = new SearchFactory();

			foreach (string input in searchList)
			{

				foreach (SearchFactory.SearchOption option in Enum.GetValues(typeof(SearchFactory.SearchOption)))
				{

					var result = await GetSearchResult(option, input);

					allResults.Add(new SearchResult
					{
						Input = input,
						option = option,
						Output = result.Output
					});

				}
			}		

			return allResults;
		}

		public async Task<SearchResult> GetSearchResult(SearchFactory.SearchOption option, string searchInput)
		{
			var resultString = await _factory.Create(option, searchInput);

			return new SearchResult
			{
				Input = searchInput,
				option = option,
				Output = resultString
			};
		}

		public void BuildWinners(IEnumerable<SearchResult> searchResult)
		{
			foreach (SearchFactory.SearchOption option in Enum.GetValues(typeof(SearchFactory.SearchOption)))
			{
				Console.WriteLine(option.ToString() + " Winner:" + GetWinner(option, searchResult));
			}

			Console.WriteLine("Total Winner:" + GetTotalWinner(searchResult));

		}

		public string GetWinner(SearchFactory.SearchOption option, IEnumerable<SearchResult> searchResult)
		{
			if (searchResult != null && searchResult.Any())
			{
				var FilterSearchResult = searchResult.Where(x => x.option.ToString().ToLower() == option.ToString().ToLower());
				return FilterSearchResult.WhereMax(max => max.Output).Input;
			}
			return string.Empty;
		}

		public string GetTotalWinner(IEnumerable<SearchResult> searchResult)
		{
			if (searchResult != null && searchResult.Any())
			{
				return searchResult.WhereMax(max => max.Output).Input;
			}
			return string.Empty;

		}

		public void PrintSearchResult(IEnumerable<SearchResult> searchResult)
		{
			int intCount = 1;
			foreach (SearchResult result in searchResult)
			{
				Console.WriteLine(result.Input + ":" + result.option.ToString() + ":" + result.Output);
				if (intCount % 2 == 0)
				{
					Console.WriteLine("\n");
				}
			}
		}

	}
}