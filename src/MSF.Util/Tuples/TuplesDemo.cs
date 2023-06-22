using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MSF.Util.Tuples
{
    public class TuplesDemo
    {
        public static void DoSomething()
        {
            var person = GetPerson();
            Debug.WriteLine($"{person.Item1}: {person.Item2}");
        }

        public static (int, string) GetPerson()
        {
            // Explicitly declare the type of each variable inside parentheses:
            (string destination, double distance) = ("post office", 3.6);

            // Implicitly declare typed variables and let the compiler infer their types
            var imp = ("post office", 3.6);
            Debug.WriteLine($"{imp.Item1}: {imp.Item2}");

            // Give a name foreach item in the tuple
            (int Min, int Max) tuple = (1, 3);
            Debug.WriteLine($"Min: {tuple.Min} Max: {tuple.Max}");

            // Tuples as out parameters
            var limitsLookup = new Dictionary<int, (int Min, int Max)>()
            {
                [2] = (4, 10),
                [4] = (10, 20),
                [6] = (0, 23)
            };

            if (limitsLookup.TryGetValue(4, out (int Min, int Max) limits))
            {
                Console.WriteLine($"Found limits: min is {limits.Min}, max is {limits.Max}");
            }

            return (1, "John Doe");
        }
    }
}
