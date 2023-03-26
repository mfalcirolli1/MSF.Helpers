using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MSF.Util.Tasks
{
    public static class TaskDemo
    {
        public static void DoJob()
        {
            for (int i = 0; i <= 10; i++)
            {
                Console.WriteLine(i);
            }
        }

        public static double Dobrar(double x)
        {
            return 2 * x;
        }

        public static void Execute()
        {
            // 1
            // Tasks alike Threads 
            Task t = new Task(DoJob);
            t.Start();

            // 2
            // Inicia automaticamente
            Task tt = Task.Run(() =>
            {
                for (int i = 0; i <= 10; i++)
                {
                    Console.WriteLine(i);
                }
            });

            // 3
            Task.Run(() => DoJob());

            // 4
            Task.Factory.StartNew(() => DoJob());

            // 5
            Task<double> db = Task.Run(() => Dobrar(22.56));
            Console.WriteLine(db.Result);

            // 6
            Task tk = new Task(() => Console.WriteLine($"Thread number: {Thread.CurrentThread.ManagedThreadId}"));
            tk.Start();

            Console.ReadLine();
        }
    }
}
