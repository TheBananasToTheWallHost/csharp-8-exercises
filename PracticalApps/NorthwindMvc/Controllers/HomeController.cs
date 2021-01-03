using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NorthwindMvc.Models;
using Packt.Shared;
using System.Threading;
using System.Net.Http;
using Newtonsoft.Json;

namespace NorthwindMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _clientFactory;
        private Northwind _db;
        private string _country;

        public HomeController(ILogger<HomeController> logger, Northwind injectedContext, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _db = injectedContext;
            _clientFactory = clientFactory;
        }
        
        public async Task<IActionResult> Customers(string country)
        {
            string uri;

            if (string.IsNullOrEmpty(country))
            {
                ViewData["Title"] = "All Customers Worldwide";
                ViewData["Country"] = "";
                uri = "api/customers";
            }
            else
            {
                ViewData["Title"] = $"Customers in {country}";
                ViewData["Country"] = country;
                uri = $"api/customers/?country={country}";
            }

            var client = _clientFactory.CreateClient("Northwind Service");
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            HttpResponseMessage respone = await client.SendAsync(request);

            string jsonString = await respone.Content.ReadAsStringAsync();

            IEnumerable<Customer> model = JsonConvert.DeserializeObject<IEnumerable<Customer>>(jsonString);

            return View(model);
        }

        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteCustomer(string id, string country){
            string uri = $"api/customers/{id}";

            var client = _clientFactory.CreateClient("Northwind Service");
            var request = new HttpRequestMessage(HttpMethod.Delete, uri);
            HttpResponseMessage response = await client.SendAsync(request);

            if(response.StatusCode == System.Net.HttpStatusCode.NoContent){
                return RedirectToAction("Customers", routeValues: new {country = country});
            }

            return BadRequest("Unable to delete the customer");
        }

        public async Task<IActionResult> UpdateCustomer(string id){
            throw new NotImplementedException();
        }
        

        public IActionResult ProductsThatCostMoreThan(decimal? price)
        {
            if (!price.HasValue)
            {
                return NotFound("You must pass a product price in the query string, for example, /Home/ProductsThatCostMoreThan?price=50");
            }

            IEnumerable<Product> model = _db.Products
                                            .Include(p => p.Category)
                                            .Include(p => p.Supplier)
                                            .AsEnumerable()
                                            .Where(p => p.UnitPrice > price);

            if (model.Count() == 0)
            {
                return NotFound($"No products cost more than {price:C}");
            }

            ViewData["MaxPrice"] = price.Value.ToString("c");

            return View(model);
        }

        public IActionResult ModelBinding()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ModelBinding(Thing thing)
        {
            var model = new HomeModelBindingViewModel()
            {
                Thing = thing,
                HasErrors = !ModelState.IsValid,
                ValidationErrors = ModelState.Values.SelectMany(state => state.Errors).Select(error => error.ErrorMessage)
            };

            return View(model);
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomeIndexViewModel()
            {
                VisitorCount = new Random().Next(1, 1001),
                Categories = await _db.Categories.ToListAsync(),
                Products = await _db.Products.ToListAsync()
            };

            return View(model);
        }

        public async Task<IActionResult> ProductDetails(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound("You must pass a product ID in the route, for example, /Home/ProductDetails/21");
            }

            var model = await _db.Products.SingleOrDefaultAsync(p => p.ProductID == id);

            if (model == null)
            {
                return NotFound($"Product with ID of {id} not found.");
            }

            return View(model);
        }

        public async Task<IActionResult> CategoryDetails(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound("You must pass a product ID in the route, for example, /Home/CategoryDetails/2");
            }

            var model = await _db.Categories.Include(c => c.Products)
                                            .SingleOrDefaultAsync(c => c.CategoryID == id);

            if (model == null)
            {
                return NotFound($"Category with ID of {id} not found.");
            }

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
