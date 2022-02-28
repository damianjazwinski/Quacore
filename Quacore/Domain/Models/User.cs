using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quacore.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Salt { get; set; }
        public DateTime CreatedAt { get; set; }
        public Profile Profile { get; set; }
        public List<Quack> Quacks { get; set; }
        public List<Mention> Mentions { get; set; }
        public List<Follow> Followers { get; set; } // user's followers
        public List<Follow> Followees { get; set; } // who user follows
        public List<Token> Tokens { get; set; }
    }
}
