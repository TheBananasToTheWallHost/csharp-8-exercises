using System;
using static System.Console;

namespace Arguments
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine($"There are {args.Length} arguments");

            foreach (string arg in args)
            {
                WriteLine(arg);
            }

            if(args.Length < 4){
                WriteLine("You must specify two colors and dimensions, e.g.");
                WriteLine("dotnet run red yeollow 80 40");
                return;
            }

            ForegroundColor = (ConsoleColor)Enum.Parse(
                enumType: typeof(ConsoleColor), 
                value: args[0], 
                ignoreCase: true);

            BackgroundColor = (ConsoleColor)Enum.Parse(
                enumType: typeof(ConsoleColor),
                value: args[1],
                ignoreCase: true
            );

            try{
                WindowWidth = Int32.Parse(args[2]);
                WindowHeight = Int32.Parse(args[3]);
            }
            catch(PlatformNotSupportedException){
                WriteLine("The current platform does not support changing the size of a console window");
            }
            

        }
    }
}
