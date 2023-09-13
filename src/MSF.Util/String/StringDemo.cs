using System;
using System.Collections.Generic;
using System.Text;

namespace MSF.Util.StringDemo
{
    public class StringDemo
    {
        public void StringDemonstration()
        {
            // Use 'String' when
            // 1. Working with Static Methods
            String.Concat("Hello, ", "world!");
            // 2. Fully Qualified name required in an environment where namespacing is an issua
            String especificNamespaceConflitHere = String.Empty;

            // Use 'string' when
            // 1. Local variables and parameters
            string greeting = "Hello";
            // 2. Readability. Keeps your code consistent with the use of other built-in types
            string one = "1";
            int two = 2;
        }
    }
}
