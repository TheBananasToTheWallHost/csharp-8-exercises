using System;
using System.IO;
using System.Xml;
using System.IO.Compression;

namespace WorkingWithStreams
{
    class Program
    {
        private static string[] callsigns = new string[]{
            "Husker", "Starbuck", "Apollo", "Boomer",
            "Bulldog", "Athena", "Helo", "Racetrack"
        };

        static void Main(string[] args)
        {
            //WorkWithText();
            WorkingWithXML();
            WorkingWithCompression();
            WorkingWithCompression(useBrotli: false);
        }

        private static void WorkWithText()
        {
            string textFile = Path.Combine(Environment.CurrentDirectory, "streams.txt");

            StreamWriter text = File.CreateText(textFile);

            foreach (string item in callsigns)
            {
                text.WriteLine(item);
            }

            text.Close();

            Console.WriteLine($"{textFile} contains {new FileInfo(textFile).Length}");

            Console.WriteLine(File.ReadAllText(textFile));
        }

        private static void WorkingWithXML()
        {
            FileStream xmlFileStream = null;
            XmlWriter xmlWriter = null;

            try
            {


                string xmlFile = Path.Combine(Environment.CurrentDirectory, "streams.xml");

                xmlFileStream = File.Create(xmlFile);
                xmlWriter = XmlWriter.Create(xmlFileStream, new XmlWriterSettings { Indent = true });

                xmlWriter.WriteStartDocument();

                xmlWriter.WriteStartElement("callsigns");

                foreach (string item in callsigns)
                {
                    xmlWriter.WriteElementString("callsign", item);
                }

                xmlWriter.WriteEndElement();

                xmlWriter.Close();
                xmlFileStream.Close();

                Console.WriteLine($"{xmlFile} contains {new FileInfo(xmlFile).Length}");

                Console.WriteLine(File.ReadAllText(xmlFile));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.GetType()}: {ex.Message}");
            }
            finally
            {
                if (xmlWriter != null)
                {
                    xmlWriter.Dispose();
                    Console.WriteLine("The XML writer's unmanaged resources have been disposed.");
                }
                if (xmlFileStream != null)
                {
                    xmlFileStream.Dispose();
                    Console.WriteLine("The file stream's unmanaged resources have been disposed");
                }
            }
        }

        private static void WorkingWithCompression(bool useBrotli = true)
        {
            string fileExtension = useBrotli ? "brotli" : "gzip";
            string filepath = Path.Combine(Environment.CurrentDirectory, $"streams.{fileExtension}");

            FileStream file = File.Create(filepath);
            Stream compressor;

            if (useBrotli)
            {
                compressor = new BrotliStream(file, CompressionMode.Compress);
            }
            else
            {
                compressor = new GZipStream(file, CompressionMode.Compress);
            }

            using (compressor)
            {
                using (XmlWriter xmlGzip = XmlWriter.Create(compressor))
                {
                    xmlGzip.WriteStartDocument();
                    xmlGzip.WriteStartElement("callsigns");

                    foreach (string item in callsigns)
                    {
                        xmlGzip.WriteElementString("callsign", item);
                    }
                    xmlGzip.WriteEndElement();
                }
            }

            Console.WriteLine($"{filepath} contains {new FileInfo(filepath).Length}");
            Console.WriteLine($"The compressed contents:");
            Console.WriteLine(File.ReadAllText(filepath));

            Console.WriteLine("reading the compressed XML file:");
            file = File.Open(filepath, FileMode.Open);

            Stream decompressor;
            if (useBrotli)
            {
                decompressor = new BrotliStream(file, CompressionMode.Decompress);
            }
            else
            {
                decompressor = new GZipStream(file, CompressionMode.Decompress);
            }

            using (decompressor)
            {
                using (XmlReader reader = XmlReader.Create(decompressor))
                {
                    while (reader.Read())
                    {
                        if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "callsign"))
                        {
                            reader.Read();
                            Console.WriteLine($"{reader.Value}");
                        }
                    }
                }
            }
        }
    }
}
