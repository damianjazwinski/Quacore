using Quacore.Domain.JwtTokens;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quacore.Domain.Responses
{
    public class RefreshTokenResponse : BaseResponse
    {
        public JwtAccessToken AccessToken { get; }

        public RefreshTokenResponse(bool isSuccess, JwtAccessToken accessToken) : base(isSuccess)
        {
            AccessToken = accessToken;
        }
    }
}
