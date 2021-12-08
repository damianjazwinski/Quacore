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
            CreateMap<JwtAccessToken, Token>()
                .ForMember(dest => dest.AccessToken, opt => opt.MapFrom(src => src.Token))
                .ForMember(dest => dest.AccessTokenExpiration, opt => opt.MapFrom(src => src.Expiration))
                .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(src => src.RefreshToken.Token))
                .ForMember(dest => dest.RefreshTokenExpiration, opt => opt.MapFrom(src => src.RefreshToken.Expiration));
        }
    }
}
