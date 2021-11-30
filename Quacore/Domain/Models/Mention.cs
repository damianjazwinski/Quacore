using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quacore.Domain.Models
{
    public class Mention
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Quack Quack { get; set; }
    }
}
