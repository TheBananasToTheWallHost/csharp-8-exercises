using System;

namespace Exercise04
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a number between 0 and 255: ");
            string firstNum = Console.ReadLine();

            Console.WriteLine("Enter a number between 0 and 255: ");
            string secondNum = Console.ReadLine();

            try
            {
                int first = int.Parse(firstNum);
                int second = int.Parse(secondNum);

                int result = first/second;
            }
            catch(System.Exception e){
                Console.WriteLine($"{e.GetType()}: {e.Message}");
            }
        }
    }
}
