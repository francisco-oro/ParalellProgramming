using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataSharingAndSynchronization
{
    internal class SpinLocking
    {
        public class BankAccount
        {
            private int balance;

            public int Balance { get => balance; private set => balance = value; }

            public void Deposit(int amount)
            {
                balance += amount;
            }
            public void Withdraw(int amount)
            {
                balance -= amount;
            }
        }

        public static void main()
        {
            var ba = new InterlockedOperations.BankAccount();
            var tasks = new List<Task>();

            SpinLock sl = new SpinLock();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (global::System.Int32 j = 0; j < 1000; j++)
                    {
                        var lockTaken = false;
                        try
                        {
                            sl.Enter(ref lockTaken);
                            ba.Deposit(100);
                        }
                        finally
                        {
                            if (lockTaken)
                            {
                                sl.Exit();
                            }
                        }
                    }
                }));
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (global::System.Int32 j = 0; j < 1000; j++)
                    {
                        var lockTaken = false;
                        try
                        {
                            sl.Enter(ref lockTaken);
                            ba.Withdraw(100);
                        }
                        finally
                        {
                            if (lockTaken)
                            {
                                sl.Exit();
                            }
                        }
                    }
                }));

            }

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine($"Final balance is {ba.Balance}");
        }
    }
}
