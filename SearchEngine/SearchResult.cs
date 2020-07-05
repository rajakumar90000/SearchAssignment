using System;
using System.Collections.Generic;
using System.Text;

namespace SearchEngine
{
     
        public class SearchResult
        {
            public string Input { get; set; }

            public SearchFactory.SearchOption option { get; set; }

            public string Output { get; set; }
        
        }
}
