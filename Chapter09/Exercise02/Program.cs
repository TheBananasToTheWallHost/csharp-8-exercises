using System;
using System.Xml.Serialization;
using System.Xml;
using System.Collections.Generic;
using Packt.Shared;
using System.IO;

namespace Exercise02
{
    class Program
    {
        static void Main(string[] args)
        {
            var listOfShapes = new List<Shape>{
                new Circle{Color = "Red", Radius = 2.5},
                new Rectangle{Color = "Blue", Height = 20.0, Width = 10.0},
                new Circle{Color = "Green", Radius = 6.0},
                new Circle{Color = "Purple", Radius = 12.3},
                new Rectangle{Color = "Blue", Height = 45.0, Width = 18.0}
            };

            string filePath = Path.Combine(Environment.CurrentDirectory, "shapes.xml");
            Stream xmlStream = File.Create(filePath);
            var serializer = new XmlSerializer(typeof(List<Shape>));

            using (xmlStream)
            {
                serializer.Serialize(xmlStream, listOfShapes);
            }

            FileStream xmlFile = File.Open(filePath, FileMode.Open);
            using (xmlFile)
            {
                var loadedShapes = (List<Shape>)serializer.Deserialize(xmlFile);
                foreach(Shape item in loadedShapes){
                    Console.WriteLine($"{item.GetType().Name} is {item.Color} and has an area of {item.Area:N2}");
                }
            }

        }
    }
}
