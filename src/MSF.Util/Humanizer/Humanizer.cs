using Humanizer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MSF.Util.Humanizer
{
    public static class HumanizerDemo
    {
        public static void Humanize()
        {
            var obj = new HumanizerModel
            {
                ID = 1234,
                Name = "Human"
            };

            var humanizedID = obj.ID.ToWords(false, WordForm.Normal);
            Console.WriteLine(humanizedID);
            // Output: mil duzentos e trinta e quatro

            string frase = "esta É uMa Frase QuAlQuer. aPenas PARa TEstEs!";

            Console.WriteLine(frase.Transform(To.TitleCase));
            Console.WriteLine(frase.Transform(To.SentenceCase));
            Console.WriteLine(frase.Transform(To.LowerCase, To.SentenceCase));
            Console.WriteLine(frase.Transform(To.LowerCase, To.SentenceCase).Truncate(16, "..."));
            Console.WriteLine(Book.HistoryAndGeography.Humanize());
            Console.WriteLine(Book.ScienceFiction.Humanize());
            Console.WriteLine(Book.HorrorFiction.Humanize());

            Book bookType = "Science Fiction".DehumanizeTo<Book>();
            Console.WriteLine(bookType);
            bookType = "Bla bla bla".DehumanizeTo<Book>();
            Console.WriteLine(bookType);

            Console.WriteLine($"Você receberá sua resposta {DateTime.Now.AddDays(20).AddSeconds(1).Humanize()}");
            Console.WriteLine($"Você receberá sua resposta {DateTime.Now.AddHours(4.5).Humanize()}");
            Console.WriteLine($"Você receberá sua resposta {TimeSpan.FromHours(4.5).Humanize(precision: 2)}");
            Console.WriteLine(54321.ToWords());
            Console.WriteLine(543.ToRoman());

            var tb = 1.Terabytes();
            Console.WriteLine(tb);
            Console.WriteLine(tb.Gigabytes);
            Console.WriteLine(tb.Megabytes);
            Console.WriteLine(tb.Kilobytes);
            Console.WriteLine(tb.Bytes);
            Console.WriteLine(tb.Bits);
        }
    }

    public class HumanizerModel 
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public enum Book
    {
        [Description("Bla bla bla")]
        HorrorFiction,
        ScienceFiction,
        SelfHelp,
        HistoryAndGeography
    }
}
