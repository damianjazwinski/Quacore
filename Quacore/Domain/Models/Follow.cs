using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quacore.Domain.Models
{
    public class Follow
    {
        public int FollowedId { get; set; }
        public int FollowerId { get; set; }
        public User Followed { get; set; }
        public User Follower { get; set; }
    }
}
