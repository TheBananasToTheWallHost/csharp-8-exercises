using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using Packt.Shared;
using Newtonsoft.Json;
using System.Threading.Tasks;
using NuJson = System.Text.Json.JsonSerializer;

namespace WorkingWithSerialization
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var people = new List<Person>{
                new Person(30000m){
                    FirstName = "Alice",
                    LastName = "Jones",
                    DateOfBirth = new DateTime(1974, 3, 13)
                },
                new Person(40000m){
                    FirstName = "Bob",
                    LastName = "Jones",
                    DateOfBirth = new DateTime(1969, 11, 13)
                },
                new Person(20000m){
                    FirstName = "Charles",
                    LastName = "Cox",
                    Children = new HashSet<Person>{
                        new Person(0m){
                            FirstName = "Sally",
                            LastName = "Cox",
                            DateOfBirth = new DateTime(2000, 7, 12)
                        }
                    }
                }
            };

            // var xs = new XmlSerializer(typeof(List<Person>));
            // string path = Path.Combine(Environment.CurrentDirectory, "people.xml");

            // using(FileStream stream = File.Create(path)){
            //     xs.Serialize(stream, people);
            // }

            // Console.WriteLine("Written {0:N0} bytes of XML to {1}", new FileInfo(path).Length, path);
            // Console.WriteLine();
            // Console.WriteLine(File.ReadAllText(path));

            // using(FileStream stream = File.Open(path, FileMode.Open)){
            //     var loadedPeople = (List<Person>)xs.Deserialize(stream);

            //     foreach(var item in loadedPeople){
            //         Console.WriteLine("{0} has {1} children", item.LastName, item.Children.Count);
            //     }
            // }

            string jsonPath = Path.Combine(Environment.CurrentDirectory, "people.json");

            using(StreamWriter jsonStream = File.CreateText(jsonPath)){
                var jss = new JsonSerializer();

                jss.Serialize(jsonStream, people);
            }

            Console.WriteLine();
            Console.WriteLine("Written {0:N0} bytes of JSON to: {1}", new FileInfo(jsonPath).Length, jsonPath);

            Console.WriteLine(File.ReadAllText(jsonPath));

            using(FileStream jsonLoad = File.Open(jsonPath, FileMode.Open)){
                var loadedPeople = (List<Person>)await NuJson.DeserializeAsync(
                    utf8Json: jsonLoad, 
                    returnType: typeof(List<Person>));

                foreach(var item in loadedPeople){
                    Console.WriteLine("{0} has {1} children.", item.LastName, item.Children?.Count);
                }
            }
        }
    }
}
