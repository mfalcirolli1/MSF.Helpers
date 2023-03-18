using Humanizer;
using System;
using System.Collections.Generic;
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
        }
    }

    public class HumanizerModel 
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
