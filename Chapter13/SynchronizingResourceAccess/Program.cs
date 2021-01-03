using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using static System.Console;

namespace SynchronizingResourceAccess
{
    class Program
    {
        static Random r = new Random();
        static string Message;
        static int Counter;
        static object conch = new object();

        static void Main(string[] args)
        {
            WriteLine("Please wait for the tasks to complete.");
            Stopwatch watch = Stopwatch.StartNew();

            Task a = Task.Factory.StartNew(MethodA);
            Task b = Task.Factory.StartNew(MethodB);

            Task.WaitAll(new Task[] { a, b });

            WriteLine();
            WriteLine($"Results: {Message}");
            WriteLine($"{Counter}");
            WriteLine($"{watch.ElapsedMilliseconds:#,##0} elapsed milliseconds");
        }

        static void MethodA()
        {
            lock (conch)
            {
                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(r.Next(2000));
                    Message += "A";
                    Write(".");
                    Interlocked.Increment(ref Counter);
                }
            }

        }

        static void MethodB()
        {
            try
            {
                Monitor.TryEnter(conch, 15000);
                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(r.Next(2000));
                    Message += "B";
                    Write(".");
                    Interlocked.Increment(ref Counter);
                }
            }
            finally{
                Monitor.Exit(conch);
            }
        }
    }
}
