using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MSF.Util.Binary
{
    public static class Binary
    {
        public static void Binario(string input)
        {
            var textToByte = Encoding.UTF8.GetBytes(input);
            var sb = new StringBuilder();

            foreach (var _byte in textToByte)
            {
                sb.Append(Convert.ToString(_byte, 2).PadLeft(8, '0') + " | ");
            }

            Debug.WriteLine($" As Text: {sb.ToString()}");
            Debug.WriteLine($" As Number: {Convert.ToString(Convert.ToInt32(input), 2).PadLeft(8, '0')}");
        }

        public static void Hexadecimal(string input)
        {
            var textToByte = Encoding.UTF8.GetBytes(input);
            var sb = new StringBuilder();

            foreach (var _byte in textToByte)
            {
                sb.Append(Convert.ToString(_byte, 8));
            }

            Debug.WriteLine($" As Text: {sb.ToString()}");
            Debug.WriteLine($" As Number: {Convert.ToString(Convert.ToInt32(input), 8)}");
        }

        public static void Octal(string input)
        {
            var textToByte = Encoding.UTF8.GetBytes(input);
            var sb = new StringBuilder();

            foreach (var _byte in textToByte)
            {
                sb.Append(Convert.ToString(_byte, 16));
            }

            Debug.WriteLine($" As Text: {sb.ToString()}");
            Debug.WriteLine($" As Number: {Convert.ToString(Convert.ToInt32(input), 16)}");
        }
    }
}
