using System;
using System.Collections.Generic;
using System.Text;

namespace MSF.Util.Singleton_vs_Static
{
    public static class Static
    {
        // Static class cannot be instantiated
        // Static class is a sealed class, therefore does not support inheritance, and static methods do not allow overriding
        // Used in classes such as extension, helper, constant, etc

        public static double CelsiusToFahrenheit(string temperatureCelsius)
        {
            double celsius = Double.Parse(temperatureCelsius);
            double fahrenheit = (celsius * 9 / 5) + 32;

            return fahrenheit;
        }
    }

    public class NonStatic
    {
        public string Name { get; private set; }

        // Even if two non-static objects are created, it will keep the same state for the static property,
        // even if multiple instances of the class are created.
        public static int QuantityOfStudents { get; private set; }

        public NonStatic(string name)
        {
            this.Name = name;
            QuantityOfStudents++;
        }
    }
}
