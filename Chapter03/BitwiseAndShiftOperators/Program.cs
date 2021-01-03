using System;
using static System.Console;

namespace BitwiseAndShiftOperators
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 10;
            int b = 6;

            WriteLine($"a & b = {a & b}");
            WriteLine($"a | b = {a | b}");
            WriteLine($"a ^ b = {a ^ b}");
        }
    }
}
