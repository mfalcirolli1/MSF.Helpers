using System;
using System.Collections.Generic;
using System.Text;

namespace MSF.Util.Nullable
{
    public class NullExec
    {

        // Working with Null in .NET 6 and C# 10

        // int?                                     -> nullable value type
        // string?                                  -> nullable reference type
        // Id?.ToString();                          -> null conditional operator
        // var m = i ?? 0;                          -> null coalescing operator
        // (list ??= new List<int>()).Add(8);       -> null coalescing assingment operator
        // <Nullable>enable</Nullable>              -> nullable annotation context
        // x!.Length                                -> null forgiving operator (use carefully)

        public void NullTest()
        {
            List<int>? list = null;
            (list ??= new List<int>()).Add(8);
            Console.WriteLine(list[0]);

            string s = null;
            string t = s ?? "S é nulo";
            Console.WriteLine(t);

            string x = "Matheus";
            Console.WriteLine(x!.Length);
            NullModel p = null;

            // if (p?.Name?.Length > 0)
            //if (p is not null && p.Name is not null && p.Name.Length > 0)
            //{
            //    Console.WriteLine(p.Name.Length);
            //}
            //else
            //{
            //    Console.WriteLine("This was false");
            //}

            Console.ReadLine();

        }
    }
}
