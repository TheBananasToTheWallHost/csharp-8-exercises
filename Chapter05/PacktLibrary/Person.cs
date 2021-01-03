using System;
using System.Collections.Generic;
using static System.Console;

namespace Packt.Shared
{
    public partial class Person : IComparable<Person>
    {
        public string Name;
        public DateTime DateOfBirth;
        public WondersOfTheAncientWorld FavoriteAncientWonder;
        public WondersOfTheAncientWorld BucketList;

        public List<Person> Children = new List<Person>();

        public const string Species = "Homo Sapiens";
        public readonly string HomePlanet = "Earth";
        public readonly DateTime Instantiated;

        public Person()
        {
            Name = "Unknown";
            Instantiated = DateTime.Now;
        }

        public Person this[int index]
        {
            get
            {
                return Children[index];
            }
            set
            {
                Children[index] = value;
            }
        }

        public virtual void WriteToConsole()
        {
            WriteLine($"{Name} was born on a {DateOfBirth:dddd}.");
        }

        public string GetOrigin()
        {
            return $"{Name} was born on {HomePlanet}.";
        }

        public (string, int) GetFruit()
        {
            return ("Apples", 5);
        }

        public (string Name, int Number) GetNamedFruit()
        {
            return (Name: "Apples", Number: 5);
        }

        public static Person Procreate(Person p1, Person p2)
        {
            var baby = new Person
            {
                Name = $"Baby of {p1.Name} and {p2.Name}"
            };
            p1.Children.Add(baby);
            p2.Children.Add(baby);

            return baby;
        }

        public Person ProcreateWith(Person partner)
        {
            return Procreate(this, partner);
        }

        public static Person operator *(Person p1, Person p2)
        {
            return Person.Procreate(p1, p2);
        }

        public static int Factorial(int number)
        {
            if (number < 0)
            {
                throw new ArgumentException(
                    $"{nameof(number)} cannot be less than zero.");
            }
            return localFactorial(number);

            int localFactorial(int localNumber)
            {
                if (localNumber < 1)
                {
                    return 1;
                }
                return localNumber * localFactorial(localNumber - 1);
            }
        }

        public event EventHandler Shout;

        public int AngerLevel;

        public void Poke(){
            AngerLevel++;
            if(AngerLevel >= 3){
                Shout?.Invoke(this, EventArgs.Empty);
            }
        }

        public int CompareTo(Person other)
        {
            return Name.CompareTo(other.Name);
        }

        public void TimeTravel(DateTime when){
            if(when <= DateOfBirth){
                throw new PersonException("If you travel back in time to a date earlier than your own birth, then the universe will explode");
            }
            else{
                WriteLine($"Welcome to {when:yyyy}");
            }
        }
    }

    public class PersonComparer : IComparer<Person>
    {
        public int Compare(Person x, Person y)
        {
            int result = x.Name.Length.CompareTo(y.Name.Length);

            if(result == 0){
                return x.Name.CompareTo(y.Name);
            }
            else{
                return result;
            }
        }
    }

    public class PersonException : Exception{
        public PersonException() : base()
        {
            
        }

        public PersonException(string message) : base(message){

        }

        public PersonException(string message, Exception innerException) : base(message, innerException){

        }
    }

    [Flags]
    public enum WondersOfTheAncientWorld : byte
    {
        None = 0b_0000_0000,
        GreatPyramidOfGiza = 0b_0000_0001,
        HangingGardensOfBabylon = 0b_0000_0010,
        StatueOfZeusAtOlympia = 0b_0000_0100,
        TempleOfArtemisAtEphesus = 0b_0000_1000,
        MausoleumAtHalicarnassus = 0b_0001_0000,
        ColossusOfRhodes = 0b_0010_0000,
        LighthouseOfAlexandria = 0b_0100_0000
    }
}
