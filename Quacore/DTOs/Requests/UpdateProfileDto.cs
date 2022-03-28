using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quacore.DTOs.Requests
{
    public class UpdateProfileDto
    {
        public string Description { get; set; }
        public string AvatarImageLink { get; set; }
        public string BannerImageLink { get; set; }
    }
}
