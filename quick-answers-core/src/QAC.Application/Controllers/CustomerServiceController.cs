using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QAC.Application.Models;
using Microsoft.AspNetCore.Cors;

namespace QAC.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerServiceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CustomerServiceController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpOptions]
        public IActionResult Options()
        {
            Response.Headers.Add("Access-Control-Allow-Origin", new[] { (string)Request.Headers["Origin"] });
            return NoContent();
        }

        // GET: api/CustomerService/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerService>> GetCustomerService(int id)
        {
            var customerService = await _context.customerServices.FindAsync(id);

            if (customerService == null)
            {
                return NotFound();
            }

            return customerService;
        }

        // GET: api/CustomerService/5
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerService>>> GetCustomerServices(int id)
        {
            return await _context.customerServices  
                            .Include(v => v.Validator)
                            .Include(t => t.Typification)
                            .ToListAsync();
        }

        // PUT: api/CustomerService/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerService(int id, CustomerService customerService)
        {
            customerService.Typification = null;
            customerService.Validator = null;
            customerService.EndService = DateTime.UtcNow;

            if (id != customerService.CustomerServiceId)
            {
                return BadRequest();
            }

            _context.Entry(customerService).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerServiceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CustomerService
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CustomerService>> PostCustomerService(CustomerService customerService)
        {
            customerService.Typification = null;
            customerService.Validator = null;
            customerService.TypificationId = 0;
            customerService.StartService = DateTime.UtcNow;

            _context.customerServices.Add(customerService);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomerService", new { id = customerService.CustomerServiceId }, customerService);
        }

        // DELETE: api/CustomerService/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomerService>> DeleteCustomerService(int id)
        {
            var customerService = await _context.customerServices.FindAsync(id);
            if (customerService == null)
            {
                return NotFound();
            }

            _context.customerServices.Remove(customerService);
            await _context.SaveChangesAsync();

            return customerService;
        }

        private bool CustomerServiceExists(int id)
        {
            return _context.customerServices.Any(e => e.CustomerServiceId == id);
        }
    }
}
