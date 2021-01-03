using Microsoft.AspNetCore.Mvc;
using Packt.Shared;
using System.Collections.Generic;
using System.Linq;

namespace NorthwindService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly Northwind _db;

        public CategoriesController(Northwind db){
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IEnumerable<Category> GetCategories(){
            var categories = _db.Categories.ToArray();
            return categories;
        }

        [HttpGet("{id}", Name = nameof(GetCategory))]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(404)]
        public Category GetCategory(int id){
            var category = _db.Categories.Find(id);
            return category;
        }

    }
}