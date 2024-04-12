using Microsoft.AspNetCore.Mvc;

namespace SpotOnAPI.V2.Controllers
{
    [ApiController]
    public class CustomerController : Controller
    {
        [HttpGet("GetCustomers")]
        public string GetCustomers()
        {
            return "Reading all Customers";
        }

        [HttpGet("GetCustomers/{id}")]
        public string GetCustomerById(int id)
        {
            return $"Reading Customer: {id}";
        }

        [HttpPost("CreateCustomer")]
        public string CreateCustomer()
        {
            return $"Creating a Customer";
        }

        [HttpDelete("DeleteCollar")]
        public string DeleteCustomer(int id)
        {
            return $"Deleting Customer: {id}";
        }
    }
}
