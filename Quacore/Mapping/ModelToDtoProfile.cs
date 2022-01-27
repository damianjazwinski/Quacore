using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using Quacore.Domain.JwtTokens;
using Quacore.Domain.Models;
using Quacore.DTOs.Requests;
using Quacore.DTOs.Responses;

using Profile = Quacore.Domain.Models.Profile;

namespace Quacore.Mapping
{
    public class ModelToDtoProfile : AutoMapper.Profile
    {
        public ModelToDtoProfile()
        {
            CreateMap<User, UserResponseDto>();
            CreateMap<JwtAccessToken, LoginResponseDto>()
                .ForMember(dest => dest.AccessToken, opt => opt.MapFrom(src => src.Token))
                .ForMember(dest => dest.AccessTokenExpirationTime, opt => opt.MapFrom(src => src.Expiration))
                .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(src => src.RefreshToken.Token))
                .ForMember(dest => dest.RefreshTokenExpirationTime, opt => opt.MapFrom(src => src.RefreshToken.Expiration));

            CreateMap<JwtAccessToken, RefreshTokenResponseDto>()
                .ForMember(dest => dest.AccessToken, opt => opt.MapFrom(src => src.Token))
                .ForMember(dest => dest.AccessTokenExpirationTime, opt => opt.MapFrom(src => src.Expiration))
                .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(src => src.RefreshToken.Token))
                .ForMember(dest => dest.RefreshTokenExpirationTime, opt => opt.MapFrom(src => src.RefreshToken.Expiration));

            CreateMap<Quack, QuackDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username));
        }
    }
}
