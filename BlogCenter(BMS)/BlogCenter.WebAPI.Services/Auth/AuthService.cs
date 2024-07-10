using BlogCenter.WebAPI.Models.Models;
using BlogCenter.WebAPI.Repositories.Auth;
using BMS.Server.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static BMS.Server.ViewModels.ServiceResponses;

namespace BlogCenter.WebAPI.Services.Auth
{
    public class AuthService(
        IAuthRepository _authRepository, 
        UserManager<ApplicationUser> _userManager,
        RoleManager<IdentityRole> _roleManager,
        IConfiguration _configuration
        ) : IAuthService
    {
        public async Task<GeneralResponse> CreateAccount(UserDto userDto)
        {
            if (userDto is null) return new GeneralResponse(false, "Model is empty");
            var newUser = new ApplicationUser()
            {
                Name = userDto.Name,
                Email = userDto.Email,
                PasswordHash = userDto.Password,
                UserName = userDto.Email
            };
            var user = _authRepository.GetUserByEmail(userDto.Email);
           
            if (user is not null) return new GeneralResponse(false, "User registered already");

            var createUser = await _userManager.CreateAsync(newUser!, userDto.Password);
            if (!createUser.Succeeded) return new GeneralResponse(false, "Error occured.. please try again");

            //Assign Default Role : Admin to first registrar; rest is user
            var checkAdmin = await _roleManager.FindByNameAsync("Admin");
            if (checkAdmin is null)
            {
                await _roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
                await _userManager.AddToRoleAsync(newUser, "Admin");
                return new GeneralResponse(true, "Account Created");
            }
            else
            {
                var checkUser = await _roleManager.FindByNameAsync("User");
                if (checkUser is null)
                    await _roleManager.CreateAsync(new IdentityRole() { Name = "User" });

                await _userManager.AddToRoleAsync(newUser, "User");
                return new GeneralResponse(true, "Account Created");
            }
        }

        public async Task<bool> isTokenValidate(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var key = _configuration["JwtSettings:SecretKey"];
            var keyBytes = System.Text.Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]);
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["JwtSettings:Issuer"],
                ValidAudience = _configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"])),
            };
            var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
            if (claimsPrincipal is null)
            {

                return false;
            }
            return true;
        }
        public async Task<LoginResponse> LoginAccount(LoginDto loginDTO)
        {
            var getUser = _authRepository.GetUserByEmail(loginDTO.Email);
            if (getUser is null)
                return new LoginResponse(false, null!, "User not found");

            bool checkUserPasswords = await _authRepository.CheckCredentialsByEmailAndPassword(getUser.Email, loginDTO.Password);
            if (!checkUserPasswords)
                return new LoginResponse(false, null!, "Invalid email/password");

            var getUserRole = await _authRepository.GetUserRoleByEmail(getUser.Email);
            var userSession = new UserSession(getUser.Id.ToString(), getUser.FirstName, getUser.Email, getUserRole);
            string token = GenerateToken(userSession);
            return new LoginResponse(true, token!, "Login completed");
        }

        public async Task<ApiResponse> TokenValidator(string token)
        {
            ApiResponse apiResponse = new ApiResponse();
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var key = _configuration["JwtSettings:SecretKey"];
            var keyBytes = System.Text.Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]);
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["JwtSettings:Issuer"],
                ValidAudience = _configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"])),
            };
            var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
            var identity = new ClaimsIdentity(claimsPrincipal.Claims, "jwt");
            var user = new ClaimsPrincipal(identity);
            return apiResponse = new()
            {
                IsSuccess = true,
                StatusCode = System.Net.HttpStatusCode.Accepted,
                Result = claimsPrincipal
            };
        }

        private string GenerateToken(UserSession user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
