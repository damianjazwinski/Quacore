using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quacore.Domain.Models
{
    public class Quack
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public User User { get; set; }
        public List<Mention> Mentions { get; set; }

    }
}
