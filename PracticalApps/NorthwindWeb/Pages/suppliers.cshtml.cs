using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Packt.Shared;
using System.IO;
using System.Linq;

namespace NorthwindWeb.Pages{
    public class SuppliersModel : PageModel{
        public IEnumerable<string> Suppliers {get;set;}
        [BindProperty]
        public Supplier Supplier{get;set;}
        private Northwind db;

        public SuppliersModel(Northwind injectedContext)
        {
            db = injectedContext;
        }

        public void OnGet(){
            ViewData["Title"] = "Northwind Web Site - Suppliers";

            Suppliers = db.Suppliers.Select(supplier => supplier.CompanyName);
        }

        public IActionResult OnPost(){
            if(ModelState.IsValid){
                db.Suppliers.Add(Supplier);
                db.SaveChanges();
                return RedirectToPage("/suppliers");

            }
            return Page();
        }
    }
}