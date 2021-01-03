using System;
using static System.Console;

namespace HandlingExceptions
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Before parsing");

            Write("What is your age: ");
            string input = ReadLine();

            try
            {
                int age = int.Parse(input);
                WriteLine($"You are {age} years old");
            }
            catch (System.FormatException e){
                WriteLine("The age you entered is not in a valid format");
            }
            catch (System.Exception e)
            {
                WriteLine($"{e.GetType()} says {e.Message}");
            }

            WriteLine("After parsing");
        }
    }
}
