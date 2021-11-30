using System.ComponentModel.DataAnnotations;

namespace Quacore.DTOs.Requests
{
    public class RegisterRequestDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }

    }
}
