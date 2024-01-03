using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskCoordination
{
    internal class ManualResetEventSlimAndAutoResetEvent
    {
        public static void main()
        {
            var evt = new ManualResetEventSlim();  

            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Boiling water");
                evt.Set();
            });

            var makeTea = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Waiting for water...");
                evt.Wait();
                Console.WriteLine("Here is your tea");
            }); 
            makeTea.Wait();
        }
    }
}
