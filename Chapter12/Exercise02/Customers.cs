using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Packt.Shared
{
    public class Customer
    {
        public string CustomerID{get;set;}
        public string CompanyName{get;set;}
        public string City{get;set;}
    }
}