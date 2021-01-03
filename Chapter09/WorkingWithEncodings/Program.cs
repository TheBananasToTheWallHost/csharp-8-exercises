using System;
using System.Text;
using System.IO;
using System.Xml;
using System.IO.Compression;

using static System.Console;

namespace WorkingWithEncodings
{
    class Program
    {
        private static string[] encodings = new string[]{
            "ASCII", "UTF-7", "UTF-8", "UTF-16", "UTF-32"
        };

        static void Main(string[] args)
        {
            WriteLine("Encodings");
            for(int i = 0; i < encodings.Length; i++){
                WriteLine($"[{i + 1} {encodings[i]}]");
            }
            WriteLine("[any other key] Default");

            Write("Press a number to choose an encoding: ");
            ConsoleKey number = ReadKey(intercept: false).Key;
            WriteLine();
            WriteLine();

            Encoding encoder = number switch{
                ConsoleKey.D1 => Encoding.ASCII,
                ConsoleKey.D2 => Encoding.UTF7,
                ConsoleKey.D3 => Encoding.UTF8,
                ConsoleKey.D4 => Encoding.Unicode,
                ConsoleKey.D5 => Encoding.UTF32,
                _ => Encoding.Default
            };

            string message = "A pint of milk is $1.99";

            byte[] encoded = encoder.GetBytes(message);
            WriteLine($"{encoder.GetType().Name} uses {encoded.Length:N0} bytes");

            WriteLine($"BYTE  HEX  CHAR");{
                foreach(byte b in encoded){
                    WriteLine($"{b, 4} {b.ToString("X"), 4} {(char)b, 5}");
                }
            }
        }

    }
}
