using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MSF.Util.StopWatch
{
    public static class StopWatchDemo
    {
        public static void StopWatchTest()
        {
            var timer = new Stopwatch();
            timer.Start();

            // Task

            timer.Stop();
            var tempoDeProcessamento = timer.Elapsed;
        }
    }
}
