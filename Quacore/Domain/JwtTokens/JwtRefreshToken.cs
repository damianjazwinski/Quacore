using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quacore.Domain.JwtTokens
{
    public class JwtRefreshToken : JsonWebToken
    {
        public JwtRefreshToken(string token, DateTime expiration) : base(token, expiration)
        {
        }
    }
}
