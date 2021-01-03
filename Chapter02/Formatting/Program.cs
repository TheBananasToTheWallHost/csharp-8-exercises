using System;
using static System.Console;

namespace Formatting
{
    class Program
    {
        static void Main(string[] args)
        {
            // int numberOfApples = 12;
            // decimal pricePerApple = 0.35M;

            // WriteLine(
            //     format: "{0} apples costs {1:C}",
            //     arg0: numberOfApples,
            //     arg1: pricePerApple * numberOfApples
            // );

            // string formatted = string.Format(
            //     format: "{0} apples costs {1:C}",
            //     arg0: numberOfApples,
            //     arg1: pricePerApple * numberOfApples
            // );
                
            // WriteLine($"{numberOfApples} apples costs {pricePerApple * numberOfApples:C}");

            // string applesText = "Apples";
            // int applesCount = 1234;
            // string bananasText = "Bananas";
            // int bananasCount =  56789;

            // WriteLine($"{"Name", -8} {"Count", 6:N0}");
            // WriteLine($"{applesText, -8} {applesCount, 6:N0}");
            // WriteLine($"{bananasText, -8} {bananasCount, 6:N0}");

            // Write("Type your first name and press ENTER: ");
            // string firstName = ReadLine();

            // Write("Type your age and press ENTER: ");
            // string age = ReadLine();

            // WriteLine($"Hello {firstName}, you look good for {age}.");

            Write("Press any key combination: ");
            ConsoleKeyInfo key = ReadKey();
            WriteLine();
            WriteLine("Key: {0}, Char: {1}, Modifiers: {2}", key.Key, key.KeyChar, key.Modifiers);

        }
    }
}
