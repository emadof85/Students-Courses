using Application.DTOs;
using Application.IServices;
using Domain.Entities;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [ApiController]
    [Route("api/[area]/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AccountController(
            IAuthService authService,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _authService = authService;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);
            if (result.Succeeded)
            {
                
                /*var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var role = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;*/

                var token = _jwtTokenGenerator.GenerateToken();
                
                return Ok(new
                {
                    Token = token,/*
                    UserId = userId,
                    Role = role*/
                });
            }
            return BadRequest();
            
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return Ok(new{message = "You have logout of the system"});
        }
    }

}
