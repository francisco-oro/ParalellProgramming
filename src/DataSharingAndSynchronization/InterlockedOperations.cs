using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataSharingAndSynchronization
{
    internal class InterlockedOperations
    {
        public class BankAccount
        {
            private int balance;

            public int Balance { get => balance; private set => balance = value; }

            public void Deposit(int amount)
            {
                Interlocked.Add(ref balance, amount);
            }
            public void Withdraw(int amount)
            {
                Interlocked.Add(ref balance, -amount);
            }
        }
    }
}
