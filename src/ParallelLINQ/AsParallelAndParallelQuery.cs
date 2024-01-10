using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelLINQ
{
    internal class AsParallelAndParallelQuery
    {
        public static void main()
        {
            const int count = 50;

            var items = Enumerable.Range(1, count).ToArray();
            var results = new int[count];

            items.AsParallel().ForAll(x =>
            {
                int newValue = x * x * x;
                Console.Write($"{newValue} ({Task.CurrentId})\t");
                results[x - 1] = newValue;
            });
            Console.WriteLine();
            Console.WriteLine();

            foreach (var i in results)
            {
                Console.WriteLine($"{i}\t");
            }

            Console.WriteLine();

            var cubes = items.AsParallel().AsOrdered().Select(x => x * x * x);

            var arr = cubes.ToArray();  
            foreach (var cube in cubes)
            {
                Console.WriteLine($"{cube}\t");
            }

            Console.WriteLine();
        }
    }
}
