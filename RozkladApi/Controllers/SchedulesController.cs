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
    public class SchedulesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public SchedulesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Schedules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScheduleDTO>>> GetSchedules()
        {
            var schedules = await _context.Schedules
                .Include(s => s.BusLine)
                .Include(s => s.Stop)
                .ToListAsync();

            // Map entities to DTOs
            var scheduleDTOs = schedules.Select(s => new ScheduleDTO
            {
                LineId = s.LineId,
                StopId = s.StopId,
                Weekdays = s.Weekdays,
                Saturdays = s.Saturdays,
                Sundays = s.Sundays
            }).ToList();

            return Ok(scheduleDTOs);
        }

        // GET: api/Schedules/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ScheduleDTO>> GetSchedule(int id)
        {
            var schedule = await _context.Schedules
                .Include(s => s.BusLine)
                .Include(s => s.Stop)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (schedule == null)
            {
                return NotFound();
            }

            var scheduleDTO = new ScheduleDTO
            {
                LineId = schedule.LineId,
                StopId = schedule.StopId,
                Weekdays = schedule.Weekdays,
                Saturdays = schedule.Saturdays,
                Sundays = schedule.Sundays
            };

            return Ok(scheduleDTO);
        }

        // PUT: api/Schedules/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchedule(int id, ScheduleDTO scheduleDTO)
        {
            if (id != scheduleDTO.LineId)
            {
                return BadRequest();
            }

            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }

            // Update the schedule using the DTO
            schedule.Weekdays = scheduleDTO.Weekdays;
            schedule.Saturdays = scheduleDTO.Saturdays;
            schedule.Sundays = scheduleDTO.Sundays;

            _context.Entry(schedule).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Schedules
        [HttpPost]
        public async Task<ActionResult<ScheduleDTO>> PostSchedule(ScheduleDTO scheduleDTO)
        {
            // Create the new schedule based on DTO
            var schedule = new Schedule
            {
                LineId = scheduleDTO.LineId,
                StopId = scheduleDTO.StopId,
                Weekdays = scheduleDTO.Weekdays,
                Saturdays = scheduleDTO.Saturdays,
                Sundays = scheduleDTO.Sundays
            };

            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();

            // Map the created schedule to DTO and return
            var createdScheduleDTO = new ScheduleDTO
            {
                LineId = schedule.LineId,
                StopId = schedule.StopId,
                Weekdays = schedule.Weekdays,
                Saturdays = schedule.Saturdays,
                Sundays = schedule.Sundays
            };

            return CreatedAtAction("GetSchedule", new { id = schedule.Id }, createdScheduleDTO);
        }

        // DELETE: api/Schedules/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }

            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ScheduleExists(int id)
        {
            return _context.Schedules.Any(e => e.Id == id);
        }
    }
}