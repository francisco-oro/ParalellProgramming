﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskProgramming
{
    internal class ExceptionHandling
    {
        public static void main()
        {
            try
            {
                Test();
            } catch (AggregateException ae)
            {
                foreach (var e in ae.InnerExceptions)
                {
                    Console.WriteLine($"Handled elsewhere {e.GetType()}");
                }
            }

            Console.WriteLine("Main program done");
            Console.ReadKey();
        }

        private static void Test()
        {
            var t = Task.Factory.StartNew(() =>
            {
                throw new InvalidOperationException("Can't do this") { Source = "t" };
            });

            var t2 = Task.Factory.StartNew(() =>
            {
                throw new AccessViolationException("Can't access this") { Source = "t2" };
            });

            try
            {
                Task.WaitAll(t, t2);
            }
            catch (AggregateException ae)
            {
                    ae.Handle(e =>
                    {
                        if (e is InvalidOperationException)
                        {
                            Console.WriteLine("Invalid op!");
                            return true;
                        }
                        else return false;
                    });
            }
        }
    }
}
