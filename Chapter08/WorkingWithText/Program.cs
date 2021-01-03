using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Collections.Immutable;

using static System.Console;

namespace WorkingWithText
{
    class Program
    {
        static void Main(string[] args)
        {
            // string name = "Jones, Alan";
            // int commaIndex = name.IndexOf(',');
            // string firstName = name.Substring(commaIndex + 2);
            // string lastName = name.Substring(0, commaIndex);

            // Console.WriteLine($"{firstName} {lastName}");

            // Write("Enter your age: ");
            // string input = ReadLine();

            // var ageChecker = new Regex(@"^\d+$");

            // if (ageChecker.IsMatch(input))
            // {
            //     WriteLine("Thank you!");
            // }
            // else
            // {
            //     WriteLine($"This is not a valid age: {input}");
            // }

            // string films = "\"Monsters, Inc.\", \"I, Tonya\", \"Lock, Stock and Two Smoking Barrels\"";
            // var csv = new Regex("(?:^|,)(?=[^\"]|(\")?)\"?((?(1)[^\"]*|[^,\"]*))\"?(?=,|$)");

            // MatchCollection filmsSmart = csv.Matches(films);

            // WriteLine("Smart attempt at splitting: ");
            // foreach(Match film in filmsSmart){
            //     WriteLine(film.Groups[2].Value);
            // }

            var cities = new List<string>(){"Milan", "Sydney", "London"};
            var immutableCities = cities.ToImmutableList();
            var newList = immutableCities.Add("Rio");
            Write("Immutable list of cities: ");
            foreach(string city in immutableCities){
                Write($" {city}");
            }
            WriteLine();

            Write("New list of cities: ");
            foreach(string city in newList){
                Write($" {city}");
            }
            WriteLine();
        }
    }
}
