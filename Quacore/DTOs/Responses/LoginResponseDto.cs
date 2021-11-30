using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quacore.DTOs.Responses
{
    public class LoginResponseDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime AccessTokenExpirationTime { get; set; }
        public DateTime RefreshTokenExpirationTime { get; set; }
    }
}
