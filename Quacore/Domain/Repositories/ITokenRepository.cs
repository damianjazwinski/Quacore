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
        public Task<Token> GetToken(string accessToken, int userId);
        public Task<Token> GetToken(string refreshToken);
    }
}
