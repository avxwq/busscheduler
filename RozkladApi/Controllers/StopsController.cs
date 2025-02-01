using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RozkladApi.Models;

namespace RozkladApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StopsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public StopsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Stops
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stop>>> GetStops()
        {
            return await _context.Stops.Include(s => s.Departures).ToListAsync();
        }

        // GET: api/Stops/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Stop>> GetStop(int id)
        {
            var stop = await _context.Stops.Include(s => s.Departures).FirstOrDefaultAsync(s => s.Id == id);

            if (stop == null)
            {
                return NotFound();
            }

            return stop;
        }

        // PUT: api/Stops/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStop(int id, Stop stop)
        {
            if (id != stop.Id)
            {
                return BadRequest();
            }

            _context.Entry(stop).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StopExists(id))
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

        // POST: api/Stops
        [HttpPost]
        public async Task<ActionResult<Stop>> PostStop(Stop stop)
        {
            _context.Stops.Add(stop);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStop", new { id = stop.Id }, stop);
        }

        // DELETE: api/Stops/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStop(int id)
        {
            var stop = await _context.Stops.FindAsync(id);
            if (stop == null)
            {
                return NotFound();
            }

            _context.Stops.Remove(stop);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StopExists(int id)
        {
            return _context.Stops.Any(e => e.Id == id);
        }
    }
}