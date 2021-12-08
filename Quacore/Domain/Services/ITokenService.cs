using Quacore.Domain.JwtTokens;
using Quacore.Domain.Models;
using Quacore.Domain.Responses;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quacore.Domain.Services
{
    public interface ITokenService
    {
        public Task<JwtAccessToken> CreateAccessToken(User user);
        public Task<RefreshTokenResponse> Refresh(string refreshToken);
        public Task<ResourceExistsResponse> AccessTokenExists(string accessToken);
    }
}
