﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataSharingAndSynchronization
{
    internal class MutexE
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

            public void Transfer(BankAccount where, int amount)
            {
                Balance -= amount;
                where.balance += amount;    
            }
        }

        public static void main()
        {
            var tasks = new List<Task>();
            var ba = new BankAccount();
            var ba2 = new BankAccount();

            Mutex mutex = new Mutex();
            Mutex mutex2 = new Mutex();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (global::System.Int32 j = 0; j < 1000; j++)
                    {

                        bool haveLock = mutex.WaitOne();
                        try
                        {
                            ba.Deposit(1);
                        }
                        finally
                        {
                            if (haveLock)
                            {
                                mutex.ReleaseMutex();
                            }
                        }
                    }
                }));                
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (global::System.Int32 j = 0; j < 1000; j++)
                    {
                        bool haveLock = mutex2.WaitOne();
                        try
                        {
                            ba2.Deposit(1);
                        }
                        finally
                        {
                            if (haveLock)
                            {
                                mutex2.ReleaseMutex();
                            }
                        }
                    }
                }));
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (global::System.Int32 j = 0; j < 1000; j++)
                    {
                        bool haveLock = WaitHandle.WaitAll(new[] { mutex, mutex2 });
                        try
                        {
                            ba.Transfer(ba2, 1);
                        }
                        finally
                        {
                            if (haveLock)
                            {
                                mutex.ReleaseMutex();
                                mutex2.ReleaseMutex();
                            }
                        }
                    }
                })); 

            }
            Task.WaitAll(tasks.ToArray());

            Console.WriteLine($"Final balance ba is {ba.Balance}");
            Console.WriteLine($"Final balance ba2 is {ba2.Balance}");
        }
    }
}
