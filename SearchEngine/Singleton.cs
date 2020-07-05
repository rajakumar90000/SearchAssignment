using System;
using System.Collections.Generic;
using System.Text;

namespace SearchEngine
{
    public sealed class Singleton
    {
        Singleton()
        {
        }
        private static readonly object padlock = new object();
        private static Singleton instance = null;
        public static Singleton Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new Singleton();
                        }
                    }
                }
                return instance;
            }
        }
    }
}
