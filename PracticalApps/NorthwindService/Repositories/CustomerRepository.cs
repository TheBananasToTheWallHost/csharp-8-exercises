using Microsoft.EntityFrameworkCore.ChangeTracking;
using Packt.Shared;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindService.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private static ConcurrentDictionary<string, Customer> _customerCache;

        private Northwind _database;

        public CustomerRepository(Northwind database){
            _database = database;

            if(_customerCache == null){
                _customerCache = new ConcurrentDictionary<string, Customer>(_database.Customers.ToDictionary(c => c.CustomerID));
            }
        }

        public async Task<Customer> CreateAsync(Customer customer){
            customer.CustomerID = customer.CustomerID.ToUpper();
            EntityEntry<Customer> added = await _database.Customers.AddAsync(customer);

            int affected = await _database.SaveChangesAsync();
            if(affected == 1){
                return _customerCache.AddOrUpdate(customer.CustomerID, customer, UpdateCache);
            }

            return null;
        }

        public Task<IEnumerable<Customer>> RetrieveAllAsync()
        {
            return Task.Run<IEnumerable<Customer>>(() => _customerCache.Values);
        }

        public Task<Customer> RetrieveAsync(string customerID)
        {
            return Task.Run(() =>
            {
                customerID = customerID.ToUpper();
                Customer customer;
                _customerCache.TryGetValue(customerID, out customer);
                return customer;
            });
        }

        private Customer UpdateCache(string customerID, Customer customer){
            Customer old;
            if(_customerCache.TryGetValue(customerID, out old)){
                if(_customerCache.TryUpdate(customerID, customer, old)){
                    return customer;
                }
            }
            return null;
        }

        public async Task<Customer> UpdateAsync(string customerID, Customer customer)
        {
            customerID = customerID.ToUpper();
            customer.CustomerID = customer.CustomerID.ToUpper();

            _database.Customers.Update(customer);
            int affected = await _database.SaveChangesAsync();

            if(affected == 1){
                return UpdateCache(customerID, customer);
            }
            return null;
        }

        public async Task<bool?> DeleteAsync(string customerID)
        {
            customerID = customerID.ToUpper();

            Customer customer = _database.Customers.Find(customerID);
            _database.Customers.Remove(customer);
            int affected = await _database.SaveChangesAsync();

            if(affected == 1){
                return _customerCache.TryRemove(customerID, out customer);
            }
            return null;
        }
    }
}