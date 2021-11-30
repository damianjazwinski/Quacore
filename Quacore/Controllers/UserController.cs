﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Quacore.Domain.Models;
using Quacore.Domain.Responses;
using Quacore.Domain.Services;
using Quacore.DTOs.Requests;
using Quacore.DTOs.Responses;

namespace Quacore.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    [Produces("application/json")]
    public class UserController : Controller
    {
        private IMapper Mapper { get; }
        private IUserService UserService { get; }
        private ITokenService TokenService { get; }

        public UserController(IMapper mapper, IUserService userService, ITokenService tokenService)
        {
            Mapper = mapper;
            UserService = userService;
            TokenService = tokenService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]RegisterRequestDto registerRequest)
        {
            var user = Mapper.Map<User>(registerRequest) 
                ?? throw new Exception("Mapper failed.");

            var response = await UserService.Register(user);

            if (!response.IsSuccess)
                return BadRequest();

            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]LoginRequestDto loginRequest)
        {
            var response = await UserService.Login(loginRequest.Username, loginRequest.Password);

            if (!response.IsSuccess)
                return BadRequest();

            var loginResponseDto = Mapper.Map<LoginResponseDto>(response.AccessToken) 
                ?? throw new Exception("Mapper failed.");

            return Ok(loginResponseDto);
        }

        [HttpGet]
        [Route("ping")]
        [Authorize]
        public IActionResult Ping()
        {
            return Ok();
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody]RefreshTokenRequestDto refreshTokenRequest)
        {
            var response = await TokenService.Refresh(refreshTokenRequest.AccessToken, refreshTokenRequest.RefreshToken);

            if (!response.IsSuccess)
                return BadRequest();

            var refreshTokenResponseDto = Mapper.Map<RefreshTokenResponse>(response.AccessToken)
                ?? throw new Exception("Mapper failed.");

            return Ok(refreshTokenResponseDto);
        }
    }
}
