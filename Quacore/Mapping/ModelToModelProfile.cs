using Quacore.Domain.JwtTokens;
using Quacore.Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quacore.Mapping
{
    public class ModelToModelProfile : AutoMapper.Profile
    {
        public ModelToModelProfile()
        {
            CreateMap<JwtRefreshToken, RefreshToken>();
            CreateMap<JwtAccessToken, AccessToken>();
        }
    }
}
