using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie_api.Models;
using Movie_api.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _Service;

        public CustomersController(ICustomerRepository Service)
        {
            _Service = Service;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomer()
        {
            var data = await _Service.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var data = await _Service.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }


        [HttpPost("")]
        public async Task<IActionResult> AddCustomer([FromBody] Customer customer)
        {
            var data = await _Service.AddAsync(customer);
            return CreatedAtAction(nameof(GetCustomerById),new { id = data, controller = "Customers" }, data);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> EditCustomer([FromBody] Customer customer,[FromRoute]int id)
        {
            var data = await _Service.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            await _Service.UpdateAsync(id,customer);
            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer( int id)
        {
            var data = await _Service.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            await _Service.DeleteAsync(id);
            return Ok(id);
        }
    }
}
