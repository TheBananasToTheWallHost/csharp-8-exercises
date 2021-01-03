using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Packt.Shared;
using System.Diagnostics;
using System.Linq;

namespace NorthwindWeb.Pages{
    public class CustomerDetailsModel : PageModel{

        private Northwind db;

        [BindProperty(SupportsGet=true)]
        public string ID{get;set;}

        [BindProperty]
        public Customer Customer{get;set;}
        
        public CustomerDetailsModel(Northwind injectedDb){
            db = injectedDb;
        }

        public void OnGet(){
            ViewData["Title"] = "Northwind Website - Customer Details";
            Debug.WriteLine(ID);

            Customer = db.Customers
                .Where(customer => customer.CustomerID == ID)
                .Include(customer => customer.Orders)
                .ThenInclude(order => order.OrderDetails)
                .ThenInclude(orderDetails => orderDetails.Product)
                .FirstOrDefault();
        }
    }
}