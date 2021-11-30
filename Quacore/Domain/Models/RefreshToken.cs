using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quacore.Domain.Models
{
    public class RefreshToken : BaseToken
    {
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
