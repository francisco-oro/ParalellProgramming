using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataSharingAndSynchronization
{
    internal class LockRecursion
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
        static SpinLock sl = new SpinLock(true);
        public static void lockRecursion(int x)
        {
            bool lockTaken = false;
            try
            {
                sl.Enter(ref lockTaken);
            } catch (LockRecursionException e)
            {
                Console.WriteLine("Exception " + e);
            }
            finally
            {
                if (lockTaken)
                {
                    Console.WriteLine($"Took a lock, x= {x}");
                    lockRecursion(x - 1);
                    sl.Exit();
                }
                else
                {
                    Console.WriteLine($"Failed to take a lock, x = {x}");
                }
            }
        }
        public static void main()
        {
            lockRecursion(5);
        }
    }
}
