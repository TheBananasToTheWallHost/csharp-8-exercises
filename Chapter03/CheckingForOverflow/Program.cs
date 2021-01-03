using System;

namespace CheckingForOverflow
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                checked
                {
                    int x = int.MaxValue - 1;
                    Console.WriteLine($"Initial value: {x}");
                    x++;
                    Console.WriteLine($"After incrementing: {x}");
                    x++;

                    Console.WriteLine($"After incrementing: {x}");
                    x++;

                    Console.WriteLine($"After incrementing: {x}");
                    x++;

                    Console.WriteLine($"After incrementing: {x}");
                    x++;
                }
            }
            catch (System.Exception)
            {
                Console.WriteLine("There was overflow but I caught the exception");
            }

            unchecked
            {
                int y = int.MaxValue + 1;

                Console.WriteLine($"Initial value: {y}");
                y--;
                Console.WriteLine($"After decrementing: {y}");
                y--;
                Console.WriteLine($"After decrementing: {y}");
                y--;
            }
        }
    }
}
