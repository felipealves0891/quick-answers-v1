using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QAC.Application.Models;

namespace QAC.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentificationValidatorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public IdentificationValidatorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/IdentificationValidator
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IdentificationValidator>>> Getvalidators()
        {
            return await _context.validators.ToListAsync();
        }

        // GET: api/IdentificationValidator/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IdentificationValidator>> GetIdentificationValidator(int id)
        {
            var identificationValidator = await _context.validators.FindAsync(id);

            if (identificationValidator == null)
            {
                return NotFound();
            }

            return identificationValidator;
        }

        // PUT: api/IdentificationValidator/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIdentificationValidator(int id, IdentificationValidator identificationValidator)
        {
            if (id != identificationValidator.Id)
            {
                return BadRequest();
            }

            _context.Entry(identificationValidator).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IdentificationValidatorExists(id))
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

        // POST: api/IdentificationValidator
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<IdentificationValidator>> PostIdentificationValidator(IdentificationValidator identificationValidator)
        {
            _context.validators.Add(identificationValidator);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIdentificationValidator", new { id = identificationValidator.Id }, identificationValidator);
        }

        // DELETE: api/IdentificationValidator/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<IdentificationValidator>> DeleteIdentificationValidator(int id)
        {
            var identificationValidator = await _context.validators.FindAsync(id);
            if (identificationValidator == null)
            {
                return NotFound();
            }

            _context.validators.Remove(identificationValidator);
            await _context.SaveChangesAsync();

            return identificationValidator;
        }

        private bool IdentificationValidatorExists(int id)
        {
            return _context.validators.Any(e => e.Id == id);
        }
    }
}
