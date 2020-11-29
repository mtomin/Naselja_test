using Naselja_test_api.Data;
using Naselja_test_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Naselja_test_api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DrzaveController : ControllerBase
    {
        private readonly NaseljaDBContext _context;

        public DrzaveController(NaseljaDBContext context)
        {
            _context = context;
        }

        // GET: api/Drzave
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Drzava>>> GetDrzava()
        {
            return await _context.Drzava.ToListAsync();
        }

        // GET: api/Drzave/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Drzava>> GetDrzava(int id)
        {
            var drzava = await _context.Drzava.FindAsync(id);

            if (drzava == null)
            {
                return NotFound();
            }

            return drzava;
        }

        // PUT: api/Drzave/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDrzava(int id, Drzava drzava)
        {
            if (id != drzava.ID)
            {
                return BadRequest();
            }

            _context.Entry(drzava).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DrzavaExists(id))
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

        // POST: api/Drzave
        [HttpPost]
        public async Task<ActionResult<Drzava>> PostDrzava(Drzava drzava)
        {
            _context.Drzava.Add(drzava);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDrzava", new { id = drzava.ID }, drzava);
        }

        // DELETE: api/Drzave/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Drzava>> DeleteDrzava(int id)
        {
            var drzava = await _context.Drzava.FindAsync(id);
            if (drzava == null)
            {
                return NotFound();
            }

            _context.Drzava.Remove(drzava);
            await _context.SaveChangesAsync();

            return drzava;
        }

        private bool DrzavaExists(int id)
        {
            return _context.Drzava.Any(e => e.ID == id);
        }
    }
}
