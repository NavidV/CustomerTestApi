using CustomerTestApi.Model;
using CustomerTestApi.Models;

namespace CustomerTestApi.Services.IServices
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetCustomerByIdAsync(int id);
        Task<Customer> CreateCustomerAsync(Customer customer);
    }
}
