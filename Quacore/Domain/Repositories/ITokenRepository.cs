using Quacore.Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quacore.Domain.Repositories
{
    public interface ITokenRepository
    {
        public Task Add(AccessToken accessToken, RefreshToken refreshToken);
        public void Remove(string accessToken, string refreshToken);
        public (AccessToken, RefreshToken) GetTokens(string accessToken, string refreshToken);
    }
}
