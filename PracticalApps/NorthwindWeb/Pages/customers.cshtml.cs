using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Packt.Shared;
using System.IO;
using System.Linq;

namespace NorthwindWeb.Pages{

    public class CustomersModel : PageModel{
        public IEnumerable<IGrouping<string, Customer>> CustomersAndCountries{get; set;}

        private Northwind db;

        public CustomersModel(Northwind injectedContext){
            db = injectedContext;
        }

        public void OnGet(){
            ViewData["Title"] = "Northwind Website - Customers";

            CustomersAndCountries = db.Customers
                                        .ToList()
                                        .GroupBy(customer => customer.Country)
                                        .OrderByDescending(group => group.Count())
                                        .ThenBy(group => group.Key);
        }
    }
}