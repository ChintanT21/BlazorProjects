using BMS.Server.AuthRepository;
using BMS.Server.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BMS.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthRepository authRepository) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto UserDto)
        {
            var response = await authRepository.CreateAccount(UserDto);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var response = await authRepository.LoginAccount(loginDto);
            return Ok(response);
        }
        [HttpPost("tokenvalidator")]
        public async Task<IActionResult> ValidateToken(string token)
        {
            var isTokenValidate = await authRepository.isTokenValidate(token);
            ApiResponse claimsprincipal = await authRepository.TokenValidator(token);
            return Ok(isTokenValidate);


        }
    }
}
