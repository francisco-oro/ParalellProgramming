using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskCoordination
{
    internal class ManualResetEventSlimAndAutoResetEvent1
    {
        public static void main()
        {
            var evt = new AutoResetEvent(false); // false

            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Boiling water");
                evt.Set(); // true 
            });

            var makeTea = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Waiting for water...");
                evt.WaitOne(); // false false
                Console.WriteLine("Here is your tea");
                var ok = evt.WaitOne(1000); // false
                if (ok)
                {
                    Console.WriteLine("Enjoy your tea");
                }
                else
                {
                    Console.WriteLine("No tea for you");
                }
            });
            makeTea.Wait();
        }
    }
}
