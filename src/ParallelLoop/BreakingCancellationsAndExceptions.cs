using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace ParallelLoop
{
    internal class BreakingCancellationsAndExceptions
    {
        private static ParallelLoopResult result;

        public static void Demo()
        {
            var cts = new CancellationTokenSource();

            ParallelOptions po = new ParallelOptions();    
            po.CancellationToken = cts.Token;
            result = Parallel.For(0, 20, po, (int x, ParallelLoopState state) =>
            {
                Console.WriteLine($"{x} [{Task.CurrentId}]\t");

                if (x == 10)
                {
                    /*throw new Exception();*/
                    /*state.Stop();*/
                    /*state.Break();*/
                    cts.Cancel();
                }
            });

            Console.WriteLine();
            Console.WriteLine($"was loop completed? {result.IsCompleted}");
            if (result.LowestBreakIteration.HasValue)
            {
                Console.WriteLine($"Lowest break iterations is {result.LowestBreakIteration}");
            }
        }
        public static void main()
        {
            try
            {
                Demo();
            }
            catch (AggregateException ex) 
            {
                ex.Handle(e =>
                {
                    Console.WriteLine(e.Message);
                    return true;
                });
            }
            catch (OperationCanceledException ex) 
            {
            
            }
        }
    }
}
