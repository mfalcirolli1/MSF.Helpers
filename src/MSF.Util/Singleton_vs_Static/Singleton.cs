using System;
using System.Collections.Generic;
using System.Text;

namespace MSF.Util.Singleton_vs_Static
{
    public class Singleton : IDisposable
    {
        // Singleton is one of the service lifetimes that can be used with Microsoft Dependency Injection
        // Uses the same instance for the whole application
        // Used in classes such as service proxies, Logger class, caching, database connections, etc.

        private static Singleton? _instance;

        public Singleton()
        {
        }

        public static Singleton GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Singleton();
            }

            return _instance;
        }

        public void Dispose()
        {
            GC.Collect();
            GC.SuppressFinalize(this);
            GC.WaitForPendingFinalizers();
        }
    }
}
