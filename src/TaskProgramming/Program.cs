using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TaskProgramming
{
    internal class Program
    {
        public static void Write(char c)
        {
            int i = 1000;
            while (i-- > 0)
            {
                Console.Write(c);
            }
        }

        public static void Write1(object o)
        {
            int i = 1000;
            while (i-- > 0)
            {
                Console.Write(o);
            }
        }

        public static int TextLength(object o)
        {
            Console.WriteLine($"\n Task with id {Task.CurrentId} processing object {o}...");
            return o.ToString().Length; 
        }

        static void Main(string[] args)
        {
            /*            string text1 = "Testing", text2 = "this";
                        var task1 = new Task<int>(TextLength, text1);
                        task1.Start();  
                        Task<int> task2 = Task.Factory.StartNew(TextLength, text2);

                        Console.WriteLine($"Length of {text1} is {task1.Result}");
                        Console.WriteLine($"Length of {text2} is {task2.Result}");

                        Console.WriteLine("Main program done.");
                        Console.ReadKey();  */
            ExceptionHandling.main();
        }
    }
}
