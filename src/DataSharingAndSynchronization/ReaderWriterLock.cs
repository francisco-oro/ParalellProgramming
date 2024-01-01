using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataSharingAndSynchronization
{
    class ReaderWriterLock
    {
		static ReaderWriterLockSlim padlock = new ReaderWriterLockSlim();
		static Random random = new Random();	
		public static void main ()
		{
			int x = 0; 
			var tasks = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
				{
					padlock.EnterReadLock();
					Console.WriteLine($"Entered read lock x = {x}");
					Thread.Sleep(5000);

					padlock.ExitReadLock();

                    Console.WriteLine($"Exited read lock, x = {x}.");
                }));
            }

			try
			{
				Task.WaitAll(tasks.ToArray());
			} catch (AggregateException ae)
			{
				ae.Handle(e =>
					{
						Console.WriteLine(e);
						return true;
					});
			}

            while (true)
            {
				Console.ReadKey();
				padlock.EnterWriteLock();
                Console.WriteLine("Write lock aquired");
				int newValue = random.Next(10);
				x = newValue;
                Console.WriteLine($"Set x = {x}");
				padlock.ExitWriteLock();
                Console.WriteLine("Write lock released");
            }
        }
	}
}
