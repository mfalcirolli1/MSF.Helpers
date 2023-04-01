using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MSF.Util.Asynchronous
{
    public static class AsyncDemo
    {
        public async static Task<string> Executar()
        {
            Console.WriteLine("Faasoida");

            Console.WriteLine("Faasoisadadasdada");

            var t = await Esperar();

            Console.WriteLine("Faasoida");

            Console.WriteLine("Faasoisadadasdada");

            return t;
        }

        public async static Task<string> Esperar()
        {
            await Task.Delay(4000);
            return "Teste";
        }
    }
}
