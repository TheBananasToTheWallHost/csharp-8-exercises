using System;
using static System.Console;
using static System.Convert;
using static System.Math;

namespace CastingConverting
{
    class Program
    {
        static void Main(string[] args)
        {
            // int a = 10;
            // double b = a;
            
            // WriteLine(b);

            // double c = 9.8;
            // int d = (int)c;
            // WriteLine(d);

            // long e = 10;
            // int f = (int) e;
            // WriteLine($"e is {e:N0} and f is {f:N0}");

            // e = long.MaxValue;
            // e = 5_000_000_000;
            // f = (int)e;
            // WriteLine($"e is {e:N0} and f is {f:N0}");

            // double g = 9.8;
            // int h = ToInt32(g);
            // WriteLine($"g is {g} and h is {h}");

            // double[] doubles = new[] {9.49, 9.5, 9.51, 10.49, 10.5, 10.51};

            // foreach(var value in doubles){
            //     WriteLine($"ToInt({value}) is {ToInt32(value)}");
            // }

            byte[] binaryObject = new byte[128];

            (new Random()).NextBytes(binaryObject);

            for(int index = 0; index < binaryObject.Length; index++){
                Write($"{binaryObject[index]:X}");
            }

            WriteLine();
            
            string encoded = Convert.ToBase64String(binaryObject);
            WriteLine($"binary object as base64: {encoded}");

            int age = int.Parse("27");
            DateTime birthday = DateTime.Parse("4 July 1980");

            WriteLine($"I was born {age} years ago");
            WriteLine($"My birthday is {birthday}.");
        }
    }
}
