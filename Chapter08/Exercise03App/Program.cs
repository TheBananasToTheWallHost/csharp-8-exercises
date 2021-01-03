using System;
using System.Numerics;
using Nanas.Extensions;

namespace Exercise03App
{
    class Program
    {
        static void Main(string[] args)
        {
            do{
            Console.WriteLine("Please enter a number to convert to words: ");
            string stringNum = Console.ReadLine();
            decimal num = decimal.Parse(stringNum);
            Console.WriteLine($"{num} as words is: ");
            Console.WriteLine(num.ToWords());
            }while(Console.ReadKey().Key != ConsoleKey.Q);
        }
    }
}
