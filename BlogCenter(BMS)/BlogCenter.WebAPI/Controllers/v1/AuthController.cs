using BlogCenter.WebAPI.Services.Auth;
using BMS.Server.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BlogCenter.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthService _authService) : ControllerBase
    {

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto UserDto)
        {
            var response = await _authService.CreateAccount(UserDto);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var response = await _authService.LoginAccount(loginDto);
            return Ok(response);
        }
    }
}

