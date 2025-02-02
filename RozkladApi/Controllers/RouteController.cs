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
    public class RoutesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public RoutesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Routes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RouteDTO>>> GetRoutes()
        {
            var routes = await _context.Routes
                .Include(r => r.Stops)  // Include Stops for each Route
                .Select(r => new RouteDTO
                {
                    Id = r.Id.ToString(),
                    Number = r.Number,
                    StartPoint = r.StartPoint,
                    EndPoint = r.EndPoint,
                    Stops = r.Stops.Select(s => new StopDto
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Location = s.Location,
                        Zone = s.Zone,
                        Departures = new DeparturesDto // Map Departures entity to DeparturesDto
                        {
                            Weekdays = s.Departures.Weekdays,
                            Weekends = s.Departures.Weekends,
                            Holidays = s.Departures.Holidays
                        }
                    }).ToList()  // Map full stop details
                })
                .ToListAsync();

            return Ok(routes);
        }

        // GET: api/Routes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<RouteDTO>> GetRoute(string id)
        {
            var route = await _context.Routes
                .Include(r => r.Stops)
                .Where(r => r.Id.ToString() == id)
                .Select(r => new RouteDTO
                {
                    Id = r.Id.ToString(),
                    Number = r.Number,
                    StartPoint = r.StartPoint,
                    EndPoint = r.EndPoint,
                    Stops = r.Stops.Select(s => new StopDto
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Location = s.Location,
                        Zone = s.Zone,
                        Departures = new DeparturesDto // Map Departures entity to DeparturesDto
                        {
                            Weekdays = s.Departures.Weekdays,
                            Weekends = s.Departures.Weekends,
                            Holidays = s.Departures.Holidays
                        }
                    }).ToList()  // Map full stop details
                })
                .FirstOrDefaultAsync();

            if (route == null)
            {
                return NotFound();
            }

            return Ok(route);
        }

        // POST: api/Routes
        [HttpPost]
        public async Task<ActionResult<RouteDTO>> PostRoute(RouteDTO routeDTO)
        {
            Random rnd = new Random();
            // Map the RouteDTO to the Route entity
            var route = new Models.Route
            {
                Number = routeDTO.Number,
                StartPoint = routeDTO.StartPoint,
                EndPoint = routeDTO.EndPoint,
                Stops = routeDTO.Stops.Select(s => new Stop
                {
                    Id = rnd.Next(20,5000),
                    Name = s.Name,
                    Location = s.Location,
                    Zone = s.Zone,
                    Departures = new Departures // Map DeparturesDto to Departures
                    {
                        Weekdays = s.Departures.Weekdays,
                        Weekends = s.Departures.Weekends,
                        Holidays = s.Departures.Holidays
                    }
                }).ToList()
            };

            _context.Routes.Add(route);
            await _context.SaveChangesAsync();

            // Return the created RouteDTO with full stop details
            var createdRouteDTO = new RouteDTO
            {
                Id = route.Id.ToString(),
                Number = route.Number,
                StartPoint = route.StartPoint,
                EndPoint = route.EndPoint,
                Stops = route.Stops.Select(s => new StopDto
                {
                    Name = s.Name,
                    Location = s.Location,
                    Zone = s.Zone,
                    Departures = new DeparturesDto // Map Departures to DeparturesDto
                    {
                        Weekdays = s.Departures.Weekdays,
                        Weekends = s.Departures.Weekends,
                        Holidays = s.Departures.Holidays
                    }
                }).ToList()
            };

            return CreatedAtAction(nameof(GetRoute), new { id = route.Id }, createdRouteDTO);
        }

        // PUT: api/Routes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoute(string id, RouteDTO routeDTO)
        {
            if (id != routeDTO.Id)
            {
                return BadRequest();
            }

            var route = await _context.Routes.FindAsync(id);
            if (route == null)
            {
                return NotFound();
            }

            // Update the route based on RouteDTO
            route.Number = routeDTO.Number;
            route.StartPoint = routeDTO.StartPoint;
            route.EndPoint = routeDTO.EndPoint;
            route.Stops = routeDTO.Stops.Select(s => new Stop
            {
                Id = s.Id,
                Name = s.Name,
                Location = s.Location,
                Zone = s.Zone,
                Departures = new Departures // Map DeparturesDto to Departures
                {
                    Weekdays = s.Departures.Weekdays,
                    Weekends = s.Departures.Weekends,
                    Holidays = s.Departures.Holidays
                }
            }).ToList();

            _context.Entry(route).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Routes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoute(string id)
        {
            var route = await _context.Routes.FindAsync(id);
            if (route == null)
            {
                return NotFound();
            }

            _context.Routes.Remove(route);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
