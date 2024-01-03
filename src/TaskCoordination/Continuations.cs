using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskCoordination
{
    internal class Continuations
    {
        public static void main()
        {
            var task = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Boiling water");
            });

            var task2 = task.ContinueWith(t =>
            {
                Console.WriteLine($"Completed task {t.Id}, pour water into cup.");
            });

            task2.Wait(); 
        }

        public static void main1()
        {
            var task = Task.Factory.StartNew(() => "Task 1");
            var task2 = Task.Factory.StartNew(() => "Task 2");
            var task3 = Task.Factory.ContinueWhenAny(new[] { task, task2 }, 
                t =>
                {
                    Console.WriteLine("Tasks completed");
                    /*foreach (var t in tasks)*/
                       Console.WriteLine(" - " + t.Result);
                    Console.WriteLine("All tasks done");
                });

            task3.Wait(); 
        }
    }
}
