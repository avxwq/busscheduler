using System.ComponentModel.DataAnnotations;

namespace RozkladApi.Models.Dto
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
