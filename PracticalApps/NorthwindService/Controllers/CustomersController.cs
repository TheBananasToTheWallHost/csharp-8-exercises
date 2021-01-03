using Microsoft.AspNetCore.Mvc;
using Packt.Shared;
using NorthwindService.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private ICustomerRepository _repository;

        public CustomersController(ICustomerRepository repository){
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Customer>))]
        public async Task<IEnumerable<Customer>> GetCustomers(string country){
            if(string.IsNullOrEmpty(country)){
                return await _repository.RetrieveAllAsync();
            }
            else{
                return (await _repository.RetrieveAllAsync()).Where(customer => customer.Country == country);
            }
        }

        [HttpGet("{id}", Name = nameof(GetCustomer))]
        [ProducesResponseType(200, Type = typeof(Customer))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCustomer(string id){
            id = id.ToUpper();
            Customer c = await _repository.RetrieveAsync(id);
            if(c == null){
                return NotFound();
            }
            return Ok(c);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Customer))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] Customer c){
            if(c == null){
                return BadRequest();
            }

            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            Customer added = await _repository.CreateAsync(c);

            return CreatedAtRoute(nameof(GetCustomer), new{id = added.CustomerID.ToLower()}, added);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(string id, [FromBody] Customer customer){
            id = id.ToUpper();
            customer.CustomerID = customer.CustomerID.ToUpper();

            if(customer == null){
                return BadRequest();
            }

            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var existing = await _repository.RetrieveAsync(id);

            if(existing == null){
                return NotFound();
            }

            await _repository.UpdateAsync(id, customer);

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(string id){
            id = id.ToUpper();
            var existing = await _repository.RetrieveAsync(id);
            if(existing == null){
                return NotFound();
            }

            bool? deleted = await _repository.DeleteAsync(id);

            if(deleted.HasValue && deleted.Value){
                return new NoContentResult();
            }
            else{
                return BadRequest($"Customer {id} was found but failed to delete");
            }
        }
    }
}