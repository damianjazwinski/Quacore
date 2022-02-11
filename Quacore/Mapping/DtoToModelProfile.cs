using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Quacore.Domain.Models;
using Quacore.DTOs.Requests;

namespace Quacore.Mapping
{
    public class DtoToModelProfile : AutoMapper.Profile
    {
        public DtoToModelProfile()
        {
            CreateMap<CreateProfileRequestDto, Profile>();
            CreateMap<RegisterRequestDto, User>();
            CreateMap<AddQuackRequestDto, Quack>();
        }
    }
}
