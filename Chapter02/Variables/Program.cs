using System;

namespace Variables
{
    class Program
    {
        static void Main(string[] args)
        {
            // object height = 1.88;
            // object name = "Amir";

            // Console.WriteLine($"{name} is {height} metres tall");

            // //int length1 = name.Length;
            // int length2 = ((string)name).Length;

            // Console.WriteLine($"{name} has {length2} characters");

            // dynamic anotherName = "Ahmed";

            // int length = anotherName.Length;

            // Console.WriteLine($"{anotherName} has {length} characters");

            // int population = 66_000_000;
            // double weight = 1.88;
            // decimal price = 4.99m;
            // string fruit = "Apples";
            // char letter = 'Z';
            // bool happy = true;

            // var population1 = 66_000_000;
            // var weight1 = 1.88;
            // var price1 = 4.99m;
            // var fruit1 = "Apples";
            // char letter1 = 'Z';
            // bool happy1 = true;

            Console.WriteLine($"default(int) =  {default(int)}");
            Console.WriteLine($"default(bool) = {default(bool)}");
            Console.WriteLine($"default(DateTime) = {default(DateTime)}");
            Console.WriteLine($"default(string) =  {default(string)}");
        }
    }
}
