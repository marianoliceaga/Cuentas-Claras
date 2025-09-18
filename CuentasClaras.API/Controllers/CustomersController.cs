using Microsoft.AspNetCore.Mvc;
using CuentasClaras.API.Services.Interfaces;
using CuentasClaras.Application.DTOs;

namespace CuentasClaras.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(Guid id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpGet("by-dni/{dni}")]
        public async Task<ActionResult<CustomerDto>> GetCustomerByDNI(string dni)
        {
            var customer = await _customerService.GetCustomerByDNIAsync(dni);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> CreateCustomer(CreateCustomerRequest request)
        {
            try
            {
                var customer = await _customerService.CreateCustomerAsync(request);
                return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerDto>> UpdateCustomer(Guid id, UpdateCustomerRequest request)
        {
            if (id != request.Id)
                return BadRequest("ID mismatch");

            try
            {
                var customer = await _customerService.UpdateCustomerAsync(request);
                return Ok(customer);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var result = await _customerService.DeleteCustomerAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("{id}/exists")]
        public async Task<ActionResult<bool>> CustomerExists(Guid id)
        {
            var exists = await _customerService.CustomerExistsAsync(id);
            return Ok(exists);
        }

        [HttpGet("dni-exists/{dni}")]
        public async Task<ActionResult<bool>> CustomerExistsByDNI(string dni)
        {
            var exists = await _customerService.CustomerExistsByDNIAsync(dni);
            return Ok(exists);
        }
    }
}
