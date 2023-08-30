using System;
using System.Collections.Generic;
using System.Text;

namespace MSF.Util.DesignPatterns.Singleton
{
    public sealed class SingletonDemo
    {
        private static SingletonDemo singleton = null;
        private static readonly object singletonLock = new object();

        public SingletonDemo() 
        {

        }

        public SingletonDemo SingleInstance
        {
            get
            {
                lock (singletonLock)
                {
                    if (singleton == null)
                    {
                        singleton = new SingletonDemo();
                    }
                    return singleton;
                }
            }
        }
    }
}
