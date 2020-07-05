using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine
{

     public class SearchFactory
    {
        public enum SearchOption
        {
            Google = 1,
            Yandex,
            Bing,
            DuckDuckgo
        }

        public SearchFactory()
        {


        }
        public  async Task<string> Create(SearchOption option, string searchWord)
        {
            ISearchOp searchOp = null;
          
            switch (option)
            {
                case SearchOption.Bing:
                    {
                        searchOp = new BingSearch();
                        searchOp.BuildSearchUrl(searchWord);
                        return await searchOp.GetSearchResult(searchWord);
                    }
                case SearchOption.Google:
                    {
                        searchOp = new GoogleSearch();
                        searchOp.BuildSearchUrl(searchWord);
                        return await searchOp.GetSearchResult(searchWord);
                    }
                case SearchOption.DuckDuckgo:
                    {
                        searchOp = new DuckDuckgoSearch();
                        searchOp.BuildSearchUrl(searchWord);                       
                        return await searchOp.GetSearchResult(searchWord);
                        
                    }
                case SearchOption.Yandex:
                    {
                        searchOp = new YandexSearch();
                        searchOp.BuildSearchUrl(searchWord);
                        return await searchOp.GetSearchResult(searchWord);
                        
                    }
            }

            return null;

        }

        

    }
}
