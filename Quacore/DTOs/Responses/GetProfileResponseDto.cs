using System;

namespace Quacore.DTOs.Responses
{
    public class GetProfileResponseDto
    {
        public string Description { get; set; }
        public string AvatarImageLink { get; set; }
        public string BannerImageLink { get; set; }
        public string Username { get; set; }
        public DateTime JoinDate { get; set; }
    }
}