using Quacore.Domain.JwtTokens;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quacore.Domain.Responses
{
    public class LoginResponse : BaseResponse
    {
        public JwtAccessToken AccessToken { get; }

        public LoginResponse(bool isSuccess, JwtAccessToken accessToken) : base(isSuccess)
        {
            AccessToken = accessToken;
        }
    }
}
