using Microsoft.AspNetCore.Mvc;
using SpotOnAPI.V2.Interfaces;
using SpotOnAPI.V2.Models;

namespace SpotOnAPI.V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        { 
            _customerRepository = customerRepository;
        }

        [HttpGet("GetCustomers")]
        public async Task<IActionResult> GetCustomers()
        {
            var x = await _customerRepository.GetCustomers();
            return Ok(x);
        }

        [HttpGet("GetCustomerById/{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var x = await _customerRepository.GetCustomerById(id);
            if(x is null) 
                return NotFound("Customer was not found :(");
            return Ok(x);
        }

        [HttpPost("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer([FromBody] Customer customer)
        {
            var customers = await _customerRepository.CreateCustomer(customer);
            return Ok(customers);
        }

        [HttpPut("EditCustomer")]
        public async Task<IActionResult> EditCustomer([FromBody]Customer customer)
        {
            var editedCustomer = await _customerRepository.EditCustomer(customer);
            if (editedCustomer is null)
                return NotFound("Customer not found :0");
            return Ok(editedCustomer);
        }

        [HttpDelete("DeleteCollar")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _customerRepository.DeleteCustomer(id);
            return Ok(customer);
        }
    }
}