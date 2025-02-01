using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RozkladApi.Models;
using RozkladApi.Models.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


[Route("api/[controller]")]
[ApiController]
public class RoutesController : ControllerBase
{
    private readonly DatabaseContext _context;
    public RoutesController(DatabaseContext context) { _context = context; }


    // GET: api/Routes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RozkladApi.Models.Route>>> GetRoutes()
    {
        
        return await _context.Routes.Include(r => r.Stops).ToListAsync();  
    }

    // GET: api/Routes/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<RozkladApi.Models.Route>> GetRoute(string id)
    {
        var route = await _context.Routes.Include(r => r.Stops).FirstOrDefaultAsync(r => r.Id == id);
        if (route == null)
        {
            return NotFound();
        }
        return route;
    }

    // POST: api/Routes
    [HttpPost]
    public async Task<ActionResult<RozkladApi.Models.Route>> PostRoute(RozkladApi.Models.Route route)
    {
        _context.Routes.Add(route);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetRoute", new { id = route.Id }, route);
    }

    // PUT: api/Routes/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRoute(string id, RozkladApi.Models.Route route)
    {
        if (id != route.Id)
        {
            return BadRequest();
        }

        _context.Entry(route).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!RouteExists(id))
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

    private bool RouteExists(string id)
    {
        return _context.Routes.Any(e => e.Id == id);
    }

}