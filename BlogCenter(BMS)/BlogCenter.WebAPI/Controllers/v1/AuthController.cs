using BlogCenter.WebAPI.Dtos;
using BlogCenter.WebAPI.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogCenter.WebAPI.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class AuthController(IAuthService _authService) : ControllerBase
    {
        [AllowAnonymous]
        [Authorize(Roles = "admin")]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto UserDto)
        {
            var response = await _authService.CreateAccount(UserDto);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (loginDto is null)
            {
                throw new ArgumentNullException(nameof(loginDto));
            }

            var response = await _authService.LoginAccount(loginDto);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("tokenvalidator")]
        public async Task<IActionResult> ValidateToken(string token)
        {
            var isvalidate = await _authService.isTokenValidate(token);
            ApiResponse userClaims = await _authService.TokenValidator(token);
            return Ok(userClaims);
        }
    }
}

