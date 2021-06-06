using Api.Models.Requests;
using Api.Models.Responses;
using AutoMapper;
using Core.Entities;
using Core.Services;
using EntityFramework.Exceptions.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPasswordService _passwordService;
        private readonly IMapper _mapper;

        public AccountController(IUserService userService,
            IPasswordService passwordService,
            IMapper mapper)
        {
            _userService = userService;
            _passwordService = passwordService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserDto dto)
        {
            try
            {
                User created = await _userService.RegisterAsync(_mapper.Map<RegisterUserDto, User>(dto));
                return Ok(_mapper.Map<User, UserResponseDto>(created));
            }
            catch (UniqueConstraintException)
            {
                return Conflict(new HttpError("DUPLICATE_ENTRY", "Username / Email is already taken!"));
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new HttpError("SERVER_ERROR", ex.Message));
            }
        }
    }
}
