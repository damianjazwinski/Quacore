using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quacore.Domain.JwtTokens
{
    public class JwtAccessToken : JsonWebToken
    {
        public JwtRefreshToken RefreshToken { get; }

        public JwtAccessToken(JwtRefreshToken jwtRefreshToken, string token, DateTime expiration) : base(token, expiration)
        {
            RefreshToken = jwtRefreshToken;
        }
    }
}
