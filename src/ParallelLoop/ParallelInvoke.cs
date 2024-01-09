﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelLoop
{
    internal class ParallelInvoke
    {
        public static IEnumerable<int> Range(int start, int end, int step)
        {
            for (int i = start; i < end; i += step)
            {
                yield return i;
            }
        }
        public static void main()
        {
            var a = new Action(() => { Console.WriteLine($"First {Task.CurrentId}"); });
            var b = new Action(() => { Console.WriteLine($"Second {Task.CurrentId}"); });
            var c = new Action(() => { Console.WriteLine($"Third {Task.CurrentId}"); });
            Parallel.Invoke(a,b,c);

            var po = new ParallelOptions();
            CancellationTokenSource cts = new CancellationTokenSource();
            po.CancellationToken = cts.Token;   
            Parallel.For(1, 11, i =>
            {
                Console.WriteLine($"{i * i}\t");
            });

            Console.WriteLine("===========================================");
            Parallel.ForEach(Range(1, 20, 3), Console.WriteLine);
        }
    }
}
