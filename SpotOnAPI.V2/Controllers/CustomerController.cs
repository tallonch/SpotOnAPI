using Microsoft.AspNetCore.Mvc;
using SpotOnAPI.V2.Interfaces;
using SpotOnAPI.V2.Models;

namespace SpotOnAPI.V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        // Creating the variables for DB connection
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        { 
            _customerRepository = customerRepository;
        }

        [HttpGet("GetCustomers")]
        public async Task<IActionResult> GetCustomers()
        {
            try
            {
                var x = await _customerRepository.GetCustomers();
                return Ok(x);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("GetCustomerById/{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            try
            {
                var x = await _customerRepository.GetCustomerById(id);
                if (x.Count == 0)
                    return NotFound("Customer was not found :(");
                return Ok(x);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer([FromBody] Customer customer)
        {
            try
            {
                var customers = await _customerRepository.CreateCustomer(customer);
                if (customers.UserId == 99999)
                    return BadRequest("Customer already exists. :(");
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
            try
            {
                var customer = await _customerRepository.DeleteCustomer(id);
                if (customer == false)
                    return NotFound("Customer was not found :(");
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}