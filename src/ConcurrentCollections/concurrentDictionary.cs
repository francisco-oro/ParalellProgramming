using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrentCollections
{
    internal class concurrentDictionary
    {
        private static ConcurrentDictionary<string, string> capitals = new ConcurrentDictionary<string, string>();

        public static void AddPairs()
        {
            bool success = capitals.TryAdd("France", "Paris");
            string who = Task.CurrentId.HasValue ? ("Task" + Task.CurrentId) : "Main thread";
            Console.WriteLine($"{who} {(success ? "added" : "did not add")} the element.");
        }
        public static void main()
        {
            Task.Factory.StartNew(AddPairs).Wait();
            AddPairs();

            /*capitals["Russia"] = "Leningrad";*/
            /*            capitals.AddOrUpdate("Russia", "Moscow", 
                            (k, old) => old + " --> Moscow");
                        Console.WriteLine($"The capital of Russia is {capitals["Russia"]}");*/

            /*capitals["Sweden"] = "Uppsala";*/
            var capOfSweden = capitals.GetOrAdd("Sweden", "Stockholm");
            Console.WriteLine($"The capital of Sweden is {capOfSweden}");

            const string toRemove = "Russia";
            string removed;
            var didRemove = capitals.TryRemove(toRemove, out removed);
            if (didRemove)
            {
                Console.WriteLine($"We just removed {removed}");
            }
            else
            {
                Console.WriteLine($"Failed to remove the capital of {toRemove}");
            }

            foreach (var capital in capitals)
            {
                Console.WriteLine($" - {capital.Value} is the capital of {capital.Key}");
            }
        }
    }
}
