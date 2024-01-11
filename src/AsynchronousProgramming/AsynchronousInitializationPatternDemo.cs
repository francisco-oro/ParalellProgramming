using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousProgramming
{
    internal class AsynchronousInitializationPatternDemo
    {
        public interface IAsyncInit
        {
            Task InitTask { get; }
        }

        public class MyClass : IAsyncInit
        {
            public MyClass()
            {
                InitTask = InitAsync();
            }
            public Task InitTask { get; }

            private async Task InitAsync()
            {
                await Task.Delay(1000);
            }
        }

        public class MyOtherClass : IAsyncInit
        {
            private readonly MyClass myClass;
            public MyOtherClass(MyClass myClass)
            {
                InitTask = InitAsync();
                this.myClass = myClass;
            }
            public Task InitTask { get; }

            private async Task InitAsync()
            {
                if (myClass is IAsyncInit ai)
                {
                    await ai.InitTask;
                }

                await Task.Delay(1000);
            }
        }

        public static async Task MainDemo()
        {
            var myClass = new MyClass();
            var oc = new MyOtherClass(myClass);
            await oc.InitTask;
        }
    }
}
