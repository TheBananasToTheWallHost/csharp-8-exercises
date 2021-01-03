using System;
using System.Text.RegularExpressions;
using static System.Console;

namespace Exercise02
{
    class Program
    {
        static void Main(string[] args)
        {
            string defaultExpression = @"\d+";
            WriteLine(@"The default regular expression checks for at least one digit (\d+)");

            ConsoleKeyInfo key;
            do
            {
                Write("Enter a regular expression (or press ENTER to use the default): ");
                string inputExpression = ReadLine();

                string actualExpression = string.IsNullOrEmpty(inputExpression) ? defaultExpression : inputExpression;
                Regex regex = new Regex(actualExpression);

                Write("Enter some input: ");
                string testInput = ReadLine();

                WriteLine($"{testInput} matches {regex.ToString()}? {regex.IsMatch(testInput)}");

                WriteLine("Press ESC to end or any key to try again.");
                key = ReadKey();
            } while (key.Key != ConsoleKey.Escape);
        }
    }
}
