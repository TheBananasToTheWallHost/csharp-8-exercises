using System;
using Packt.Shared;
using static System.Console;

namespace PeopleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Person p = new Person();

            // p.Name = "Bob Smith";
            // p.DateOfBirth = new DateTime(year: 1965, month: 12, day: 25);
            // p.FavoriteAncientWonder = WondersOfTheAncientWorld.StatueOfZeusAtOlympia;
            // p.BucketList = WondersOfTheAncientWorld.HangingGardensOfBabylon | WondersOfTheAncientWorld.MausoleumAtHalicarnassus;

            // Person alice = new Person{
            //     Name = "Alice Jones",
            //     DateOfBirth = new DateTime(1998, 3, 7),
            //     FavoriteAncientWonder = WondersOfTheAncientWorld.LighthouseOfAlexandria
            // };

            // WriteLine(
            //     format: "{0} was born on {1:dddd, d MMMM yyyy}",
            //     p.Name,
            //     p.DateOfBirth
            // );

            // WriteLine(
            //     format: "{0}'s favorite wonder is {1}. It's integer is {2}.",
            //     p.Name,
            //     p.FavoriteAncientWonder,
            //     (int)p.FavoriteAncientWonder
            // );

            // WriteLine(
            //     $"{p.Name}'s bucket list is {p.BucketList}"
            // );

            // WriteLine(
            //     format: "{0} was born on {1:dd MMM yy}",
            //     alice.Name,
            //     alice.DateOfBirth
            // );

            // (string, int) fruit = p.GetFruit();

            // WriteLine($"{fruit.Item1}, {fruit.Item2} there are.");
            // var f2 = p.GetNamedFruit();
            // WriteLine($"There are {f2.Number} {f2.Name}");

            // var thing1 = ("Neville", 4);
            // WriteLine($"{thing1.Item1} has {thing1.Item2} children");

            // var thing2 = (p.Name, p.Children.Count);
            // WriteLine($"{thing2.Name} has {thing2.Count} children");

            // Person harry = new Person{
            //     Name = "Harry"
            // };

            // harry.Shout += Harry_Shout;

            // harry.Poke();
            // harry.Poke();
            // harry.Poke();
            // harry.Poke();

            // Person[] people = {
            //     new Person {Name = "Simon"},
            //     new Person {Name = "Jenny"},
            //     new Person {Name = "Adam"},
            //     new Person {Name = "Richard"}
            // };

            // WriteLine("Initial list of people:");
            // foreach(var person in people){
            //     WriteLine($"{person.Name}");
            // }
            
            // WriteLine("Use Person's IComparable implementation to sort:");
            // Array.Sort(people);
            // foreach(var person in people){
            //     WriteLine($"{person.Name}");
            // }

            // WriteLine("Use PersonComparer's IComparer implementation to sort:");
            // Array.Sort(people, new PersonComparer());
            // foreach(var person in people){
            //     WriteLine($"{person.Name}");
            // }

            // var t1 = new Thing();
            // t1.Data = 42;
            // WriteLine($"Thing with an integer: {t1.Process(42)}");

            // var t2 = new Thing();
            // t2.Data = "apple";
            // WriteLine($"Thing with a string: {t2.Process("apple")}");

            // var gt1 = new GenericThing<int>();
            // gt1.Data = 42;
            // WriteLine($"Generic thing with an integer: {gt1.Process(42)}");
            
            // var gt2 = new GenericThing<string>();
            // gt2.Data = "apple";
            // WriteLine($"Generic thing with a string: {gt2.Process("apple")}");

            var dv1 = new DisplacementVector(3, 5);
            var dv2 = new DisplacementVector(-2, 7);
            var dv3 = dv1 + dv2;
            WriteLine($"({dv1.X}, {dv1.Y}) + ({dv2.X}, {dv2.Y}) = {(dv3.X, dv3.Y)}");

            Person john = new Person{
                Name = "John",
                DateOfBirth = new DateTime(1987, 11, 25)
            };

            try{
                john.TimeTravel(new DateTime(1999, 12, 31));
                john.TimeTravel(new DateTime(1950, 12, 25));
            }
            catch(PersonException e){
                WriteLine(e.Message);
            }
        }

        private static void Harry_Shout(object sender, EventArgs e){
            Person p = (Person)sender;
            WriteLine($"{p.Name} is this angry: {p.AngerLevel}");
        }
    }
}
