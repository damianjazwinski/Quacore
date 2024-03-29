﻿using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Quacore.Domain.Models;
using Quacore.Domain.Services;
using Quacore.DTOs.Requests;
using Quacore.DTOs.Responses;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quacore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuackController : ControllerBase
    {
        private IQuackService QuackService { get; }
        private IMapper Mapper { get; }

        public QuackController(IMapper mapper, IQuackService quackService)
        {
            Mapper = mapper;
            QuackService = quackService;
        }

        [Route("add")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add([FromBody]AddQuackRequestDto addQuackRequestDto)
        {
            var quack = Mapper.Map<Quack>(addQuackRequestDto) 
                ?? throw new Exception("Mapper failed.");
            quack.UserId = int.Parse(HttpContext.User.FindFirst("User").Value);
            var serviceResponse = await QuackService.Add(quack);

            if (!serviceResponse.IsSuccess)
                return BadRequest();
            var quackDto = Mapper.Map<QuackDto>(quack);
            return Ok(quackDto);
        }

        [Route("feed")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetFeed([FromQuery(Name = "startingId")]int? startingId)
        {
            // TODO: Póki co wyświetla na feed tylko quacki zalogowanego usera, trzeba jego ziomków jeszcze wyświetlić i ogarnąć czy zostało więcej.
            var userId = int.Parse(HttpContext.User.FindFirst("User").Value);
            var serviceResponse = await QuackService.GetFeed(startingId, userId);

            if (!serviceResponse.IsSuccess)
                return BadRequest();

            var quacks = Mapper.Map<IEnumerable<QuackDto>>(serviceResponse.Quacks);

            return Ok(new GetFeedResponseDto { AreAnyQuacksLeft = serviceResponse.AreAnyQuacksLeft, Quacks = quacks });
        }

        public Task<IActionResult> Delete()
        {
            throw new NotImplementedException();
        }

        [Route("get")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetQuacks([FromQuery(Name = "startingId")] int? startingId, [FromQuery(Name = "username")]string username)
        {
            var serviceResponse = await QuackService.GetByUser(startingId, username);

            if (!serviceResponse.IsSuccess)
                return BadRequest();

            var quacks = Mapper.Map<IEnumerable<QuackDto>>(serviceResponse.Quacks);

            return Ok(new GetFeedResponseDto { AreAnyQuacksLeft = serviceResponse.AreAnyQuacksLeft, Quacks = quacks });
        }

    }
}
