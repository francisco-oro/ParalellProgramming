using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSharingAndSynchronization
{
    internal class Program
    {
        public class BankAccount
        {
            public object padlock = new object();
            public int Balance { get; private set; }

            public void Deposit(int amount)
            {
                /* 
                    This is a non-atomic operation
                    += 
                    op1: temp <- getBalance() + amount
                    op2: setBalance(temp)
                 */
                lock (padlock)
                {
                    Balance += amount;
                }
            }            
            public void Withdraw(int amount)
            {
                lock (padlock)
                {
                    Balance -= amount;
                }
            }
        }
        static void Main(string[] args)
        {
            /*            var ba = new InterlockedOperations.BankAccount();
                        var tasks = new List<Task>();
                        for (int i = 0; i < 10; i++)
                        {
                            tasks.Add(Task.Factory.StartNew(() =>
                            {
                                for (global::System.Int32 j = 0; j < 1000; j++)
                                {
                                    ba.Deposit(100);
                                }
                            }));
                            tasks.Add(Task.Factory.StartNew(() =>
                            {
                                for (global::System.Int32 j = 0; j < 1000; j++)
                                {
                                    ba.Withdraw(100);
                                }
                            }));

                        }

                        Task.WaitAll(tasks.ToArray());

                        Console.WriteLine($"Final balance is {ba.Balance}");*/
            ReaderWriterLock.main();
        }
    }
}
