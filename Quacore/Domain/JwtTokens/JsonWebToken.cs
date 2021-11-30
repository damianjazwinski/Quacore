using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quacore.Domain.JwtTokens
{
    public abstract class JsonWebToken
    {
        public string Token { get; protected set; }
        public DateTime Expiration { get; protected set; }

        protected JsonWebToken(string token, DateTime expiration)
        {
            if (string.IsNullOrWhiteSpace(token)) throw new ArgumentException("Invalid token value.");

            Token = token;
            Expiration = expiration;
        }

        public bool IsExpired() => DateTime.UtcNow > Expiration;
    }
}
