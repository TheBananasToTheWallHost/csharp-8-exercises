using static System.Console;
using System;
using Packt.Shared;
using System.Linq;
using System.Text;

namespace MonitoringApp
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            WriteLine("Processing. Please wait...");
            Recorder.Start();

            int[] largeIntArray = Enumerable.Range(1, 10_000).ToArray();

            System.Threading.Thread.Sleep(new Random().Next(5, 10) * 1000);
            Recorder.Stop();
            */

            int[] numbers = Enumerable.Range(1, 50_000).ToArray();

            Recorder.Start();
            WriteLine("Using string with +");
            string s = "";
            for(int i = 0; i < numbers.Length; i++){
                s += numbers[i] + ", ";
            }
            Recorder.Stop();

            Recorder.Start();
            WriteLine("Using stringbuilder");
            var builder = new StringBuilder();
            for(int i = 0; i < numbers.Length; i++){
                builder.Append(numbers[i]);
                builder.Append(", ");
            }
            Recorder.Stop();
        }
    }
}
