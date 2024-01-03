using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskCoordination
{
    internal class barrier
    {
        static Barrier _barrier = new Barrier(2, b =>
        {
            Console.WriteLine($"Phase {b.CurrentPhaseNumber} is finished");
        });

        public static void Water()
        {
            Console.WriteLine("Putting the kettle on (takes a bit longer)");
            Thread.Sleep(2000);
            _barrier.SignalAndWait(); // 2
            Console.WriteLine("Pouring water into the cup"); // 0
            _barrier.SignalAndWait(); // 1
            Console.WriteLine("Putting the kettle away");
        }

        public static void Cup()
        {
            Console.WriteLine("Finding the nicest cup of tea (fast)");
            _barrier.SignalAndWait(); // 1 
            Console.WriteLine("Adding tea.");
            _barrier.SignalAndWait(); // 2
            Console.WriteLine("Adding sugar.");

        }

        public static void main()
        {
            var water = Task.Factory.StartNew(Water);
            var cup = Task.Factory.StartNew(Cup);

            var tea = Task.Factory.ContinueWhenAll(new[] { water, cup }, tasks =>
            {
                Console.WriteLine("Enjoy your cup of tea. ");
            });
            tea.Wait(); 
        }
    }
}
