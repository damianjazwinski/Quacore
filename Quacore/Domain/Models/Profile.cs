using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quacore.Domain.Models
{
    public class Profile
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string AvatarImageLink { get; set; }
        public string BannerImageLink { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
