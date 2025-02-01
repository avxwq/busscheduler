using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RozkladApi.Models;
using RozkladApi.Models.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RozkladApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public UsersController(DatabaseContext context)
        {
            _context = context;
        }

                // POST: api/Users/Register
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(UserDto userDto)
        {
            // Check if user already exists
            var existingUser = await _context.Users
                .AnyAsync(u => u.Email == userDto.Email || u.Username == userDto.Username);

            if (existingUser)
            {
                return BadRequest("User already exists with the given email or username.");
            }

            var user = new User
            {
                Email = userDto.Email,
                Username = userDto.Username,
                Password = userDto.Password // Store password as is (should be hashed in a real-world app)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            userDto.Id = user.Id; // Set the Id after the user is created
            return CreatedAtAction(nameof(GetUser), new { id = userDto.Id }, userDto);
        }

                // POST: api/Users/Login
        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(LoginDto loginDto)
        {
            var user = await _context.Users
                                      .FirstOrDefaultAsync(u => u.Username == loginDto.Username || u.Email == loginDto.Email);

            if (user == null || user.Password != loginDto.Password) // Compare the passwords (in a real-world scenario, hash this)
            {
                return Unauthorized("Invalid credentials");
            }

            return Ok("Login successful");
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _context.Users
                                       .Select(u => new UserDto
                                       {
                                           Id = u.Id,
                                           Email = u.Email,
                                           Username = u.Username
                                       })
                                       .ToListAsync();

            return users;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _context.Users
                                      .Where(u => u.Id == id)
                                      .Select(u => new UserDto
                                      {
                                          Id = u.Id,
                                          Email = u.Email,
                                          Username = u.Username
                                      })
                                      .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<UserDto>> PostUser(UserDto userDto)
        {
            var user = new User
            {
                Email = userDto.Email,
                Username = userDto.Username,
                // Password should be hashed before storing
                Password = userDto.Password // In real-world scenarios, hash the password
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            userDto.Id = user.Id; // Set the Id after the user is created
            return CreatedAtAction(nameof(GetUser), new { id = userDto.Id }, userDto);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserDto userDto)
        {
            if (id != userDto.Id)
            {
                return BadRequest();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.Email = userDto.Email;
            user.Username = userDto.Username;
            // Don't forget to hash the password if it's being updated
            user.Password = userDto.Password;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
