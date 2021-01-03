using Packt.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NorthwindService.Repositories
{
    public interface ICustomerRepository
    {
         Task<Customer> CreateAsync(Customer customer);
         Task<IEnumerable<Customer>> RetrieveAllAsync();
         Task<Customer> RetrieveAsync(string customerID);
         Task<Customer> UpdateAsync(string customerID, Customer customer);
         Task<bool?> DeleteAsync(string customerID);
    }
}