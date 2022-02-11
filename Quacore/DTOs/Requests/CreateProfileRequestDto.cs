using System.ComponentModel.DataAnnotations;

namespace Quacore.DTOs.Requests
{
    public class CreateProfileRequestDto
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public string AvatarImageLink { get; set; }
        [Required]
        public string BannerImageLink { get; set; }
    }
}