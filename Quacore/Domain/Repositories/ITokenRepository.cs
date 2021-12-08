using Quacore.Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quacore.Domain.Repositories
{
    public interface ITokenRepository
    {
        public void Add(Token token);
        public void Remove(Token token);
        public Task<Token> GetTokenByAccessTokenString(string accessToken);
        public Task<Token> GetTokenByRefreshTokenString(string refreshToken);
    }
}
