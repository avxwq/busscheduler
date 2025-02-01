using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RozkladApi.Models;
using RozkladApi.Models.Dto;

[Route("api/[controller]")]
[ApiController]
public class BusLinesController : ControllerBase
{
    private readonly DatabaseContext _context;

    public BusLinesController(DatabaseContext context) 
    {
        _context = context;
    }

    // GET: api/BusLines
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BusLineDto>>> GetBusLines()
    {
        var busLines = await _context.BusLines
            .Include(b => b.LineStops)
            .ThenInclude(ls => ls.BusStop)
            .Select(b => new BusLineDto
            {
                Id = b.Id,
                LineNumber = b.LineNumber,
                Description = b.Description,
                LineStops = b.LineStops.Select(ls => new BusLineStopDto
                {
                    BusStopId = ls.BusStopId,
                    BusStopName = ls.BusStop.Name,
                    TravelTime = ls.TravelTime
                }).ToList()
            })
            .ToListAsync();

        return busLines;
    }

    // GET: api/BusLines/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BusLineDto>> GetBusLine(int id)
    {
        var busLine = await _context.BusLines
            .Include(b => b.LineStops)
            .ThenInclude(ls => ls.BusStop)
            .Where(b => b.Id == id)
            .Select(b => new BusLineDto
            {
                Id = b.Id,
                LineNumber = b.LineNumber,
                Description = b.Description,
                LineStops = b.LineStops.Select(ls => new BusLineStopDto
                {
                    BusStopId = ls.BusStopId,
                    BusStopName = ls.BusStop.Name,
                    TravelTime = ls.TravelTime
                }).ToList()
            })
            .FirstOrDefaultAsync();

        if (busLine == null)
        {
            return NotFound();
        }

        return busLine;
    }

    // POST: api/BusLines
    [HttpPost]
    public async Task<ActionResult<BusLineDto>> PostBusLine(BusLineDto busLineDto)
    {
        var busLine = new BusLine
        {
            LineNumber = busLineDto.LineNumber,
            Description = busLineDto.Description
        };

        _context.BusLines.Add(busLine);
        await _context.SaveChangesAsync();

        // You might also want to add line stops after creating the bus line
        busLineDto.Id = busLine.Id; // Set the Id after the bus line is created
        return CreatedAtAction(nameof(GetBusLine), new { id = busLineDto.Id }, busLineDto);
    }

    // PUT: api/BusLines/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBusLine(int id, BusLineDto busLineDto)
    {
        if (id != busLineDto.Id)
        {
            return BadRequest();
        }

        var busLine = await _context.BusLines.FindAsync(id);
        if (busLine == null)
        {
            return NotFound();
        }

        busLine.LineNumber = busLineDto.LineNumber;
        busLine.Description = busLineDto.Description;

        _context.Entry(busLine).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/BusLines/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBusLine(int id)
    {
        var busLine = await _context.BusLines.FindAsync(id);
        if (busLine == null)
        {
            return NotFound();
        }

        _context.BusLines.Remove(busLine);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
