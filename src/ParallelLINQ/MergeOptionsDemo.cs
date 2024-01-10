using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelLINQ
{
    internal class MergeOptionsDemo
    {
        public static void MainDemo()
        {
            var numbers = Enumerable.Range(1, 20).ToArray();

            var results = numbers
                .AsParallel()
                .WithMergeOptions(ParallelMergeOptions.FullyBuffered)
                .Select(
                x =>
                {
                    var result = Math.Log10(x);
                    Console.Write($"P {result}\t");
                    return result;
                });
            foreach (var result in results)
            {
                Console.Write($"C {result}\t");
            }
        }
    }
}
