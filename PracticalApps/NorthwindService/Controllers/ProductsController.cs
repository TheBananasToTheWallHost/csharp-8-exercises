using Microsoft.AspNetCore.Mvc;
using Packt.Shared;
using System.Linq;
using System.Collections.Generic;

namespace NorthwindService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly Northwind _db;

        public ProductsController(Northwind db){
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        public IEnumerable<Product> GetAllProducts(){
            var products = _db.Products.ToArray();
            return products;
        }

        [HttpGet("{categoryID}", Name = nameof(GetCategoryProducts))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        public IEnumerable<Product> GetCategoryProducts(int categoryID){
            var products = _db.Products
                                    .Where(product => product.CategoryID == categoryID)
                                    .ToArray();

            return products;
        }
    }
}