using System;
using System.Xml.Linq;
using Packt.Shared;
using DialectSoftware.Collections.Generics;
using DialectSoftware.Collections;

namespace AssembliesAndNamespaces
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Enter a color value in hex: ");
            // string hex = Console.ReadLine();

            // Console.WriteLine("Is {0} a valid color value? {1}", hex, hex.IsValidHex());

            // Console.WriteLine("Enter a XML tag: ");
            // string xmlTag = Console.ReadLine();

            // Console.WriteLine("Is {0} a valid XML tag? {1}", xmlTag, xmlTag.isValidXMLTag());

            // Console.WriteLine("Enter a password: ");
            // string password = Console.ReadLine();

            // Console.WriteLine("Is {0} a valid password? {1}", password, password.IsValidPassword());

            var x = new Axis("x", 0, 10, 1);
            var y = new Axis("y", 0, 4, 1);

            var matrix = new Matrix<long>(new[] {x, y});
            for(int i = 0; i < matrix.Axes[0].Points.Length; i++){
                matrix.Axes[0].Points[i].Label = "x" + i.ToString();
            }

            for(int i = 0; i < matrix.Axes[1].Points.Length; i++){
                matrix.Axes[1].Points[i].Label = "y" + i.ToString();
            }

            foreach(long[] l in matrix){
                matrix[l] = l[0] + l[1];
            }

            foreach(long[] l in matrix){
                Console.WriteLine(
                    "{0}, {1} ({2}, {3}) = {4}", 
                    matrix.Axes[0].Points[l[0]].Label,
                    matrix.Axes[1].Points[l[1]].Label,
                    l[0], l[1], matrix[l]);
            }
        }
    }
}
