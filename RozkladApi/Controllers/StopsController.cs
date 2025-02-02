using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RozkladApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<ActionResult<IEnumerable<StopDto>>> GetStops()
        {
            var stops = await _context.Stops.Include(s => s.Departures).ToListAsync();

            // Mapowanie listy Stop na StopDto
            var stopsDto = stops.Select(s => new StopDto
            {
                Id = s.Id,
                Name = s.Name,
                Location = s.Location,
                Zone = s.Zone,
                Departures = new DeparturesDto
                {
                    Weekdays = s.Departures.Weekdays,
                    Weekends = s.Departures.Weekends,
                    Holidays = s.Departures.Holidays
                }
            }).ToList();

            return stopsDto;
        }

        // GET: api/Stops/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<StopDto>> GetStop(int id)
        {
            var stop = await _context.Stops.Include(s => s.Departures).FirstOrDefaultAsync(s => s.Id == id);

            if (stop == null)
            {
                return NotFound();
            }

            // Mapowanie Stop na StopDto
            var stopDto = new StopDto
            {
                Id = stop.Id,
                Name = stop.Name,
                Location = stop.Location,
                Zone = stop.Zone,
                Departures = new DeparturesDto
                {
                    Weekdays = stop.Departures.Weekdays,
                    Weekends = stop.Departures.Weekends,
                    Holidays = stop.Departures.Holidays
                }
            };

            return stopDto;
        }

        // PUT: api/Stops/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStop(int id, StopDto stopDto)
        {
            if (id != stopDto.Id)
            {
                return BadRequest();
            }

            // Mapowanie StopDto na Stop
            var stop = new Stop
            {
                Id = stopDto.Id,
                Name = stopDto.Name,
                Location = stopDto.Location,
                Zone = stopDto.Zone,
                Departures = new Departures
                {
                    Weekdays = stopDto.Departures.Weekdays,
                    Weekends = stopDto.Departures.Weekends,
                    Holidays = stopDto.Departures.Holidays
                }
            };

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
        public async Task<ActionResult<StopDto>> PostStop(StopDto stopDto)
        {
            // Mapowanie StopDto na Stop
            var stop = new Stop
            {
                Name = stopDto.Name,
                Location = stopDto.Location,
                Zone = stopDto.Zone,
                Departures = new Departures
                {
                    Weekdays = stopDto.Departures.Weekdays,
                    Weekends = stopDto.Departures.Weekends,
                    Holidays = stopDto.Departures.Holidays
                }
            };

            _context.Stops.Add(stop);
            await _context.SaveChangesAsync();

            // Mapowanie Stop na StopDto
            var createdStopDto = new StopDto
            {
                Id = stop.Id,
                Name = stop.Name,
                Location = stop.Location,
                Zone = stop.Zone,
                Departures = new DeparturesDto
                {
                    Weekdays = stop.Departures.Weekdays,
                    Weekends = stop.Departures.Weekends,
                    Holidays = stop.Departures.Holidays
                }
            };

            return CreatedAtAction("GetStop", new { id = stop.Id }, createdStopDto);
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