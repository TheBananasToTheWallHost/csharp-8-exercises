using System;
using Packt.Shared;
using System.Xml;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.IO;
using System.Text;

namespace Exercise02
{
    class Program
    {
        static void Main(string[] args)
        {
            QueryAllCategoriesAndProducts();
        }

        private static void QueryAllCategoriesAndProducts()
        {
            using (var db = new Northwind())
            {
                IQueryable<Category> categories = db.Categories.Include(category => category.Products);
                GenerateXMLFile(categories);
                GenerateXMLFile(categories, false);
                GenerateJSONFile(categories);
            }
        }

        private delegate void WriteXMLDataDelegate(string name, string value);

        private static void GenerateXMLFile(IQueryable<Category> categories, bool useAttributes = true)
        {
            string writeMethod = useAttributes ? "attributes" : "elements";

            string fileName = $"category-and-products-with-{writeMethod}.xml";
            string path = Path.Combine(Environment.CurrentDirectory, fileName);

            var xmlFileSettings = new XmlWriterSettings();
            xmlFileSettings.Indent = true;

            XmlWriter xmlFile = XmlWriter.Create(path, xmlFileSettings);

            using (xmlFile)
            {
                WriteXMLDataDelegate xmlWriteMethod;

                if (useAttributes)
                {
                    xmlWriteMethod = xmlFile.WriteAttributeString;
                }
                else
                {
                    xmlWriteMethod = xmlFile.WriteElementString;
                }

                xmlFile.WriteStartDocument();
                xmlFile.WriteStartElement("categories");                    //<categories>

                foreach (Category category in categories)
                {
                    xmlFile.WriteStartElement("category");                  //<category>
                    xmlWriteMethod("id", category.CategoryID.ToString());
                    xmlWriteMethod("name", category.CategoryName);
                    xmlWriteMethod("description", category.Description);
                    xmlFile.WriteStartElement("products");                   //<products>
                    foreach (Product product in category.Products)
                    {
                        xmlFile.WriteStartElement("product");               //<product>
                        xmlWriteMethod("id", product.ProductID.ToString());
                        xmlWriteMethod("name", product.ProductName);
                        xmlWriteMethod("cost", product.Cost.ToString());
                        xmlWriteMethod("stock", product.Stock.ToString());
                        xmlWriteMethod("discontinued", product.Discontinued.ToString());
                        xmlFile.WriteEndElement(); //</product>
                    }
                    xmlFile.WriteEndElement(); //</products>
                    xmlFile.WriteEndElement(); //</category>
                }
                xmlFile.WriteEndElement(); //</categories>
                xmlFile.WriteEndDocument();
            }
            Console.WriteLine("{0} uses {1} bytes", fileName, new FileInfo(fileName));

        }

        private static void GenerateJSONFile(IQueryable<Category> categories)
        {
            string fileName = "categories-and-products.json";
            string path = Path.Combine(Environment.CurrentDirectory, fileName);

            FileStream stream = File.Create(path);

            var file = new StreamWriter(stream, Encoding.UTF8);
            var jsonWriter = new JsonTextWriter(file);
            jsonWriter.Indentation = 4;

            using (stream)
            {
                using (file)
                {
                    using (jsonWriter)
                    {
                        jsonWriter.WriteStartObject();
                        jsonWriter.WritePropertyName("categories");
                        jsonWriter.WriteStartArray();
                        foreach (Category category in categories)
                        {
                            jsonWriter.WritePropertyName("category");
                            jsonWriter.WriteStartObject();
                            jsonWriter.WritePropertyName("id");
                            jsonWriter.WriteValue(category.CategoryID);
                            jsonWriter.WritePropertyName("name");
                            jsonWriter.WriteValue(category.CategoryName);
                            jsonWriter.WritePropertyName("description");
                            jsonWriter.WriteValue(category.Description);
                            jsonWriter.WritePropertyName("products");
                            jsonWriter.WriteStartArray();

                            foreach (Product product in category.Products)
                            {
                                jsonWriter.WritePropertyName("product");
                                jsonWriter.WriteStartObject();
                                jsonWriter.WritePropertyName("id");
                                jsonWriter.WriteValue(product.ProductID);
                                jsonWriter.WritePropertyName("name");
                                jsonWriter.WriteValue(product.ProductName);
                                jsonWriter.WritePropertyName("cost");
                                jsonWriter.WriteValue(product.Cost);
                                jsonWriter.WritePropertyName("stock");
                                jsonWriter.WriteValue(product.Stock);
                                jsonWriter.WritePropertyName("discontinued");
                                jsonWriter.WriteValue(product.Discontinued);
                                jsonWriter.WriteEndObject();
                            }
                            jsonWriter.WriteEndArray();
                            jsonWriter.WriteEndObject();
                        }
                        jsonWriter.WriteEndArray();
                        jsonWriter.WriteEndObject();
                    }
                }
            }
            Console.WriteLine("{0} uses {1} bytes", fileName, new FileInfo(fileName));
        }
    }
}
