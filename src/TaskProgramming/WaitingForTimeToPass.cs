using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskProgramming
{
    internal class WaitingForTimeToPass
    {
        public static void main()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            var t = new Task(() =>
            {
                Console.WriteLine("Press any key to disarm; you have 5 seconds");
                bool candelled = token.WaitHandle.WaitOne(5000);
                Console.WriteLine(candelled ? "Bomb disarmed." : "BOOOOM!");
            }, token);
            t.Start();

            Console.ReadKey();
            cts.Cancel();

            Console.WriteLine("Main program done");
            Console.ReadKey();
        }
    }
}
