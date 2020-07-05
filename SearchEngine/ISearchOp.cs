using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine
{
	public interface  ISearchOp
	{
		
		string BuildSearchUrl(string SearchWord);
		Task<string>  GetSearchResult(string SearchWord);

	}
}
