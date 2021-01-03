using static System.Console;
using System;
using Packt.Shared;
using System.Linq;



namespace Exercise02
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomerPrompt();
        }

        private static void CustomerPrompt()
        {
            QueryCustomerCities();
            Write("Enter the name of a city: ");
            string city = ReadLine();
            QueryCityCompanyNames(city);
        }

        private static void QueryCustomerCities()
        {
            using (var db = new Northwind())
            {
                var query = db.Customers
                                .Select(customer => customer.City)
                                .Distinct();
                                //.OrderBy(city => city);

                foreach (var city in query)
                {
                    Write($"{city}, ");
                }
            }

            WriteLine();
        }

        private static void QueryCityCompanyNames(string city)
        {
            using (var db = new Northwind())
            {
                var query = db.Customers
                                .Where(customer => customer.City == city);


                WriteLine($"There are {query.Count()} customers in {city}");
                foreach (Customer customer in query)
                {
                    WriteLine(customer.CompanyName);
                }
            }
        }
    }
}
