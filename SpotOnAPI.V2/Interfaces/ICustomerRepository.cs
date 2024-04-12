using SpotOnAPI.V2.Models;

namespace SpotOnAPI.V2.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> CreateCustomer(Customer customer);
        Task<Customer> EditCustomer(Customer customer);
        Task<bool> DeleteCustomer(int id);
        Task<List<Customer>> GetCustomers();
        Task<List<Customer>> GetCustomerById(int id);
    }
}
