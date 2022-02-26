using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Quacore.Domain.Services;
using Quacore.DTOs.Requests;
using Quacore.DTOs.Responses;
using Quacore.Services;
using Profile = Quacore.Domain.Models.Profile;

namespace Quacore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : Controller
    {
        private IMapper Mapper { get; }
        private IProfileService ProfileService { get; }

        public ProfileController(IProfileService profileService, IMapper mapper)
        {
            ProfileService = profileService;
            Mapper = mapper;
        }

        [HttpGet]
        [Route("{username}")]
        [Authorize]
        public async Task<IActionResult> Get(string username)
        {
            var serviceResponse = await ProfileService.GetByUsernameAsync(username);
            if (!serviceResponse.IsSuccess)
                return BadRequest();
            
            var response = Mapper.Map<GetProfileResponseDto>(serviceResponse.Profile);
            return Ok(response);
        }
    }
}
