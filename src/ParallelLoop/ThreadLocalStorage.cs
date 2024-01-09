using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelLoop
{
    internal class ThreadLocalStorage
    {
        public static void MainLocal()
        {
            int sum = 0;

            Parallel.For(1, 1001, 
                () => 0,
                (x, state, tls) =>
                {
                    tls += x;
                    Console.WriteLine($"{Task.CurrentId } has the sum {tls}");
                    return tls; 
                },
                partialSum =>
                {
                    Console.WriteLine($"Partial value of task {Task.CurrentId} is {partialSum}");
                    Interlocked.Add(ref sum, partialSum);
                });
            Console.WriteLine($"Sum of 1..100 = {sum}");
        }
    }
}
