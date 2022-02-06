using System.ComponentModel.DataAnnotations;

namespace Quacore.DTOs.Requests
{
    public class RegisterRequestDto
    {
        [Required]
        [RegularExpression(@"^[A-Za-z][A-Za-z0-9_]{3,40}$", ErrorMessage = "Illegal username syntax")]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }

    }
}
