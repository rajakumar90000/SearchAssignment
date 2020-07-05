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

				for (int i = 0; i <= inputs.Length - 1; i++) 
				{
					searchList.Add(inputs[i]);
				}

				var result = BuildSearchResult(searchList).GetAwaiter().GetResult();

				PrintSearchResult(result);

				BuildWinners(result);

			//	GetTotalWinner(result);

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

			if (searchResult != null && searchResult.Any())
			{
				var winners = from result in searchResult
							  group result by result.option into g
							  let topSearch = g.Max(x => Int64.Parse(x.Output))
							  select new
							  {
								  option = g.Key,
								  input = g.First(y =>Int64.Parse(y.Output) == topSearch).Input,
								  output = topSearch
							  };
				Console.WriteLine("******Who is a winner in Each options: *****\n");

					foreach (var winner in winners)
					{

						Console.WriteLine(winner.option.ToString() + " Winner:" + winner.input);
					}

				Console.WriteLine("*************************\n");
				Console.WriteLine("\n");
				Console.WriteLine("******Who is a Total winner :********\n");
			}

			Console.WriteLine("Total Winner:" + GetTotalWinner(searchResult));

			Console.WriteLine("*************************\n");
		}

		public string GetTotalWinner(IEnumerable<SearchResult> searchResult)
		{
			
				if (searchResult != null && searchResult.Any())
				{
					var maxResult = from result in searchResult
									group result by result.Input into g
									select new
									{
										input = g.Key,
										TotalResult = g.Sum(x => Int64.Parse(x.Output))
									};

				var winner = from result in maxResult
							 group result by result.TotalResult into g
							 orderby g.Key descending
							 select new
							 {
								 TotalResult = g.Key,
								 input = g.Select(x => x.input).First()
							 };

					return winner != null ?
						winner.FirstOrDefault().input.ToString() : string.Empty;
				}
			return string.Empty;

		}

		public  void PrintSearchResult(IEnumerable<SearchResult> searchResult)
		{
			int intCount = 1;
			Console.WriteLine("*************************\n");
			foreach (SearchResult result in searchResult)
			{
				Console.WriteLine(result.Input + ":" + result.option.ToString() + ":" + result.Output);
				if (intCount % 4 == 0)
				{
					Console.WriteLine("*************************\n");
					//Console.WriteLine("\n");
				}

				intCount++;
			}
		}

	}
}