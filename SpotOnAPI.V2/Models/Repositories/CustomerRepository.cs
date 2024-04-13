using Microsoft.EntityFrameworkCore;
using SpotOnAPI.V2.Interfaces;

namespace SpotOnAPI.V2.Models.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public readonly SpotOnDbContext _databaseContext;

        public CustomerRepository(SpotOnDbContext dBcontext) 
        {
            _databaseContext = dBcontext;
        }

        public async Task<Customer> CreateCustomer(Customer customer)
        {
            try
            {
                _databaseContext.Customers.Add(customer);
                await _databaseContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Customer bad = new Customer();
                bad.UserId = 99999;
                return bad;
            }
            return customer;
        }

        public async Task<bool> DeleteCustomer(int customerId)
        {
            var rows = await _databaseContext.Customers.Where(x => x.UserId == customerId).ExecuteDeleteAsync();
            if (rows == 0)
                return false;
            return true;
        }

        public async Task<Customer> EditCustomer(Customer customer)
        {
            var dbCustomer = await _databaseContext.Customers.Where(x => x.UserId == customer.UserId)
                .ExecuteUpdateAsync(x => x.SetProperty(x => x.Username, customer.Username)
                    .SetProperty(x => x.FirstName, customer.FirstName)
                    .SetProperty(x => x.LastName, customer.LastName)
                    .SetProperty(x => x.Password, customer.Password)
                    );
            if (dbCustomer == 0)
                return null;
            return customer;
        }

        public async Task<List<Customer>> GetCustomers()
        {
            var result = await _databaseContext.Customers.ToListAsync();

            return result;
        }

        public async Task<List<Customer>> GetCustomerById(int id)
        {
            var result = await _databaseContext.Customers.Where(x => x.UserId == id).ToListAsync();

            return result;
        }
    }
}
