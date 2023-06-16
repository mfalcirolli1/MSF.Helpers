using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MSF.Util.Delegates
{
    public class DelegateDemo
    {
        /* 
            Delegate holds a reference to a method. Invoke a method when it has a reference to it.
            Can be used to create flexible and extendable architectures, because they alloy methods to be passed as parameters
            Used for 'void' methods
        */

        public delegate void HelloWorld(string message);

        public void GeneralMethod(HelloWorld hw)
        {
            var msg = "Olá";
            Debug.WriteLine(msg);
            hw(msg);
        }

        public static void ReferencedMethod(string message)
        {
            message += ", Mundo";
            Debug.WriteLine(message);
        }
    }
}
