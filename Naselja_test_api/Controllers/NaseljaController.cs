using Naselja_test_api.Data;
using Naselja_test_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Naselja_test_api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NaseljaController : ControllerBase
    {
        private readonly NaseljaDBContext _context;

        public NaseljaController(NaseljaDBContext context)
        {
            _context = context;
        }

        // GET: api/Naselja
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Naselje>>> GetNaselje()
        {
            return await _context.Naselje.ToListAsync();
        }

        // GET: api/Naselja/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Naselje>> GetNaselje(int id)
        {
            var naselje = await _context.Naselje.FindAsync(id);

            if (naselje == null)
            {
                return NotFound();
            }

            return naselje;
        }

        // GET: api/NaseljaPage
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Naselje>>> GetNaseljePage(int page, int pageSize)
        {
            return await (_context.Naselje.Include(n => n.Drzava)).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        [HttpGet]
        public int GetBrojNaselja()
        {
            // SP selects number of rows from schema - faster + no locks
            SqlParameter[] @params = { new SqlParameter("@returnVal", SqlDbType.Int) { Direction = ParameterDirection.Output } };

            _context.Database.ExecuteSqlCommand("exec @returnVal=[dbo].[GetBrojNaselja]", @params);

            int brojNaselja = (int)(@params[0].Value);
            return brojNaselja;
        }

        // PUT: api/Naselja/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNaselje(int id, Naselje naselje)
        {
            if (id != naselje.ID)
            {
                return BadRequest();
            }
            naselje.Drzava = _context.Drzava.First(d => d.ID.Equals(naselje.Drzava.ID));
            _context.Entry(naselje).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NaseljeExists(id))
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

        // POST: api/Naselja
        [HttpPost]
        public async Task<ActionResult<Naselje>> PostNaselje(Naselje naselje)
        {
            naselje.Drzava = _context.Drzava.First(d => d.ID.Equals(naselje.Drzava.ID));
            _context.Naselje.Add(naselje);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNaselje", new { id = naselje.ID }, naselje);
        }

        // DELETE: api/Naselja/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Naselje>> DeleteNaselje(int id)
        {
            var naselje = await _context.Naselje.FindAsync(id);
            if (naselje == null)
            {
                return NotFound();
            }

            _context.Naselje.Remove(naselje);
            await _context.SaveChangesAsync();

            return naselje;
        }

        private bool NaseljeExists(int id)
        {
            return _context.Naselje.Any(e => e.ID == id);
        }
    }
}
