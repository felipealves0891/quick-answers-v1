using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QAC.Application.Models;
using QAC.Application.Models.TypificationTree;

namespace QAC.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypificationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TypificationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Typification
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Typification>>> Gettypifications()
        {
            return await _context.typifications.ToListAsync();
        }

        [Route("Tree")]
        [HttpGet]
        public ActionResult<IEnumerable<TypificationComponent>> getTree() 
        {
            var typifications = _context.typifications.ToListAsync();

            var tree = new TypificationTree(typifications.Result);

            var t = tree.AssembleComponent();

            return t;
        }

        // GET: api/Typification/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Typification>> GetTypification(int id)
        {
            var typification = await _context.typifications.FindAsync(id);

            if (typification == null)
            {
                return NotFound();
            }

            return typification;
        }

        // PUT: api/Typification/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypification(int id, Typification typification)
        {
            if (id != typification.Id)
            {
                return BadRequest();
            }

            _context.Entry(typification).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypificationExists(id))
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

        // POST: api/Typification
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Typification>> PostTypification(Typification typification)
        {
            _context.typifications.Add(typification);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTypification", new { id = typification.Id }, typification);
        }

        // DELETE: api/Typification/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Typification>> DeleteTypification(int id)
        {
            var typification = await _context.typifications.FindAsync(id);
            if (typification == null)
            {
                return NotFound();
            }

            _context.typifications.Remove(typification);
            await _context.SaveChangesAsync();

            return typification;
        }

        private bool TypificationExists(int id)
        {
            return _context.typifications.Any(e => e.Id == id);
        }
    }
}
