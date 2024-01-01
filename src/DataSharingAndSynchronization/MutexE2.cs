using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataSharingAndSynchronization
{
    internal class MutexE2
    {
        public static void main()
        {
            const string appName = "MyApp";
            Mutex mutex;

            try
            {
                mutex = Mutex.OpenExisting(appName);
                Console.WriteLine($"Sorry, {appName} is already running");
            } catch(WaitHandleCannotBeOpenedException e)
            {
                Console.WriteLine("We can run the program just fine");
                mutex = new Mutex(false, appName);
            }

            Console.ReadKey(); 
            mutex.ReleaseMutex();
        }
    }
}
