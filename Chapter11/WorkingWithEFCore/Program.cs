using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Packt.Shared;

using static System.Console;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace WorkingWithEFCore
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
                GenerateJSONFile(categories, JSONProvider.Newtonsoft);
                GenerateCSVFile(categories);
            }
        }

        private delegate void WriteXMLDataDelegate(string name, string value);

        private static void GenerateCSVFile(IQueryable<Category> categories){
            string fileName = "category-and-products.csv";
            string path = Path.Combine(Environment.CurrentDirectory, fileName);

            var fileStream = File.Create(path);
            var csvFile = new StreamWriter(fileStream, Encoding.UTF8);

            using(fileStream){
                using(csvFile){
                    csvFile.WriteLine("CategoryID,CategoryName,Description,ProductID,ProductName,Cost,Stock, Discontinued");
                    
                    foreach(Category category in categories){
                        foreach(Product product in category.Products){
                            csvFile.WriteLine("{0},\"{1}\",\"{2}\",{3},\"{4}\",{5},{6},{7}",
                                                category.CategoryID.ToString(),
                                                category.CategoryName,
                                                category.Description,
                                                product.ProductID.ToString(),
                                                product.ProductName,
                                                product.Cost.Value.ToString(),
                                                product.Stock.Value.ToString(),
                                                product.Discontinued.ToString());
                        }
                    }
                }
            }
            Console.WriteLine("{0} uses {1} bytes", fileName, new FileInfo(fileName).Length);
        }

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
            Console.WriteLine("{0} uses {1} bytes", fileName, new FileInfo(fileName).Length);

        }

        private static void GenerateJSONFile(IQueryable<Category> categories, JSONProvider provider = JSONProvider.NET)
        {
            string providerName = provider.ToString();
            string fileName = $"categories-and-products-{providerName}.json";
            string path = Path.Combine(Environment.CurrentDirectory, fileName);

            FileStream stream = File.Create(path);

            

            switch (provider)
            {
                case JSONProvider.NET:
                    var jsonWriterNet = new Utf8JsonWriter(stream, new JsonWriterOptions(){Indented = true});
        
                    using(jsonWriterNet){
                        jsonWriterNet.WriteStartObject();
                        jsonWriterNet.WriteStartArray("categories");

                        foreach(Category category in categories){
                            jsonWriterNet.WriteStartObject();
                            jsonWriterNet.WriteNumber("id", category.CategoryID);
                            jsonWriterNet.WriteString("name", category.CategoryName);
                            jsonWriterNet.WriteString("description", category.Description);
                            jsonWriterNet.WriteStartArray("products");
                            foreach(Product product in category.Products){
                                jsonWriterNet.WriteStartObject();
                                jsonWriterNet.WriteNumber("id", product.ProductID);
                                jsonWriterNet.WriteString("name", product.ProductName);
                                jsonWriterNet.WriteNumber("cost", product.Cost.Value);
                                jsonWriterNet.WriteNumber("stock", product.Stock.Value);
                                jsonWriterNet.WriteBoolean("discontinued", product.Discontinued);
                                jsonWriterNet.WriteEndObject();
                            }
                            jsonWriterNet.WriteEndArray();
                            jsonWriterNet.WriteEndObject();
                        }
                        jsonWriterNet.WriteEndArray();
                        jsonWriterNet.WriteEndObject();
                    }
                    break;
                case JSONProvider.Newtonsoft:
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
                    break;
            }

            Console.WriteLine("{0} uses {1} bytes", fileName, new FileInfo(fileName).Length);
        }

        private enum JSONProvider
        {
            NET,
            Newtonsoft
        }

        private static bool AddProduct(int categoryID, string productName, decimal? price)
        {
            using (var db = new Northwind())
            {
                var newProduct = new Product
                {
                    CategoryID = categoryID,
                    ProductName = productName,
                    Cost = price
                };

                db.Products.Add(newProduct);

                int affected = db.SaveChanges();
                return affected == 1;
            }
        }

        private static bool IncreaseProductPrice(string name, decimal amount)
        {
            using (var db = new Northwind())
            {
                Product updateProduct = db.Products.First(p => p.ProductName.StartsWith("name"));

                updateProduct.Cost += amount;

                int affected = db.SaveChanges();
                return (affected == 1);
            }
        }

        private static int DeleteProducts(string name)
        {
            using (var db = new Northwind())
            {
                using (IDbContextTransaction transaction = db.Database.BeginTransaction())
                {
                    WriteLine("Transaction islocation level: {0}", transaction.GetDbTransaction().IsolationLevel);
                    IEnumerable<Product> products = db.Products.Where(p => p.ProductName.StartsWith(name));
                    db.Products.RemoveRange(products);

                    int affected = db.SaveChanges();
                    transaction.Commit();
                    return affected;
                }
            }
        }

        private static void ListProducts()
        {
            using (var db = new Northwind())
            {
                WriteLine("{0, -3} {1, -35} {2, 8} {3, 5} {4}",
                "ID", "Product Name", "Cost", "Stock", "Disc");

                foreach (var item in db.Products.OrderByDescending(p => p.Cost))
                {
                    WriteLine("{0:000} {1, -35} {2, 8:$#,##0.00} {3, 5} {4}",
                    item.ProductID, item.ProductName, item.Cost, item.Stock, item.Discontinued);
                }
            }
        }

        private static void QueryingCategories()
        {
            using (var db = new Northwind())
            {
                var loggerFactory = db.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(new ConsoleLoggerProvider());

                WriteLine("Categories and how many products they have: ");
                IQueryable<Category> cats; //= db.Categories.Include(c => c.Products);

                db.ChangeTracker.LazyLoadingEnabled = false;

                WriteLine("Enable eager loading (Y/N): ");
                bool eagerLoading = (ReadKey().Key == ConsoleKey.Y);
                WriteLine();

                bool explicitLoading = false;

                if (eagerLoading)
                {
                    cats = db.Categories.Include(c => c.Products);
                }
                else
                {
                    cats = db.Categories;
                    Write("Enable explicit loading? (Y/N): ");
                    explicitLoading = (ReadKey().Key == ConsoleKey.Y);
                    WriteLine();
                }

                foreach (Category cat in cats)
                {
                    if (explicitLoading)
                    {
                        Write($"Explicitly load products for {cat.CategoryName}? (Y/N): ");
                        ConsoleKeyInfo key = ReadKey();
                        WriteLine();

                        if (key.Key == ConsoleKey.Y)
                        {
                            var products = db.Entry(cat).Collection(c2 => c2.Products);
                            if (!products.IsLoaded)
                            {
                                products.Load();
                            }
                        }
                    }
                    WriteLine($"{cat.CategoryName} has {cat.Products.Count} products");
                }
            }
        }

        private static void QueryingWithLike()
        {
            using (var db = new Northwind())
            {
                var loggerFactory = db.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(new ConsoleLoggerProvider());

                Write("Enter part of a product name: ");
                string input = ReadLine();

                IQueryable<Product> products = db.Products.Where(p => EF.Functions.Like(p.ProductName, $"%{input}%"));

                foreach (Product product in products)
                {
                    WriteLine("{0} has {1} units in stock. Discontinued? {2}",
                    product.ProductName, product.Stock, product.Discontinued);

                }
            }
        }

        private static void QueryingProducts()
        {
            using (var db = new Northwind())
            {
                var loggerFactory = db.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(new ConsoleLoggerProvider());

                WriteLine("Products that cost more than a price, highest at top.");
                string input;
                decimal price;
                do
                {
                    Write("Enter a product price: ");
                    input = ReadLine();
                } while (!decimal.TryParse(input, out price));

                IOrderedEnumerable<Product> prods = db.Products
                    .AsEnumerable()
                    .Where(product => product.Cost > price)
                    .OrderByDescending(product => product.Cost);

                foreach (Product prod in prods)
                {
                    WriteLine("{0}: {1} costs {2:$#,##0.00} and has {3} stock.",
                    prod.ProductID, prod.ProductName, prod.Cost, prod.Stock);

                }
            }
        }
    }
}
