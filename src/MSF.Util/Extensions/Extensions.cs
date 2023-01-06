using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSF.Util.Extensions
{
    public static class StringExtensions
    {
        public static void PrintBlue(this string str )
        {
            var defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(str);
            Console.ForegroundColor = defaultColor;
        }

        public static void PrintRed(this string str)
        {
            var defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(str);
            Console.ForegroundColor = defaultColor;
        }

        public static void PrintGreen(this string str)
        {
            var defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(str);
            Console.ForegroundColor = defaultColor;
        }
    }

    public static class DateTimeExtensions
    {
        public static bool BetweenInclusive(this DateTime @this, DateTime dataInicio, DateTime dataFinal)
        {
            bool dentroDoPeriodo;

            dataInicio = dataInicio.AddDays(-1).Date;
            dataFinal = dataFinal.AddDays(1).Date;

            dentroDoPeriodo = @this.Between(dataInicio, dataFinal);
            return dentroDoPeriodo;
        }

        public static bool BetweenExclusive(this DateTime @this, DateTime dataInicio, DateTime dataFinal)
        {
            bool dentroDoPeriodo;

            dentroDoPeriodo = @this.Between(dataInicio, dataFinal);
            return dentroDoPeriodo;
        }

        public static DateTime AddBusinessdays(this DateTime originalDate, int days, DateTime[] holidays)
        {
            DateTime finalDate = originalDate;
            int modifier = days > 0 ? 1 : -1;

            while(days != 0)
            {
                finalDate = finalDate.AddDays(modifier);

                if (IsWorkDay(finalDate, holidays))
                {
                    days -= modifier;
                }
            }
            return finalDate;
        }

        public static bool IsWorkDay(this DateTime dt, DateTime[] holidays = null)
        {
            return (

                dt.DayOfWeek == DayOfWeek.Sunday ||
                dt.DayOfWeek == DayOfWeek.Saturday || 
                CheckHoliday(dt, holidays)

                ) == false;
        }

        public static bool CheckHoliday(DateTime dt, DateTime[] holidays = null)
        {
            if (holidays == null) return false;
            return holidays.Contains(dt.Date);
        }

        public static string GetDate(this string input)
        {
            return DateTime.Today.ToString("dd/MM/yyyy");
        }
    }

    public static class LogExtensions
    {
        public static void LogWarning(this string message)
        {
            var defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine(message);

            Console.ForegroundColor = defaultColor;
            Console.WriteLine("Done");
        }

        public static void LogSuccess(this string message)
        {
            var defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine(message);

            Console.ForegroundColor = defaultColor;
            Console.WriteLine("Done");
        }

        public static void LogError(this string message)
        {
            var defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(message);

            Console.ForegroundColor = defaultColor;
            Console.WriteLine("Done");
        }
    }
}
