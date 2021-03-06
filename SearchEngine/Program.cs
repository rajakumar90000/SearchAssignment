﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngine
{
	class Program
	{
		//  List<string> searchList; 

		static void Main(string[] args)
		{

            if (args.Length == 0)
            {
                Console.WriteLine("Search Input Parameters is Empty"); // Check for null array
                return;
            }

            var inputs = new string[args.Length];
			int i = 0;
			args.ToList().ForEach(x => { inputs[i] = x; i++; });
		
			SearchFactory factory = new SearchFactory();
			SearchBuilder buildobj = new SearchBuilder(factory);
			buildobj.GenerateSearchResult(inputs);

		}

	}
}