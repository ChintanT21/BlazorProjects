using BMS.Client.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace BMS.Client.Components.Pages.Login
{
    public partial class Login
    {
        [Inject]
        HttpClient? Client { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private readonly IConfiguration _configuration;


        protected LoginDto loginDto = new();  
        string responseBody = string.Empty;
        string unauthorizedMessage = string.Empty;

        public async Task AuthenticateUser()
        {
            if (loginDto is null)
            {
                throw new ArgumentNullException(nameof(loginDto));
            }
            var response = await Client.PostAsJsonAsync("https://localhost:7185/api/Auth/login", loginDto);
     
                NavigationManager.NavigateTo("/dashboard");
                var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
                var token = loginResponse.token;
                await TokenService(token);
                await SecurelyStoreToken(token);
                responseBody = await response.Content.ReadAsStringAsync();


                //var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
         
            
        }
        protected async Task SecurelyStoreToken(string token)
        {
            await localStorage.SetItemAsync("authToken", token);
        }

        public async Task TokenService(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key2 = _configuration["JwtSettings:SecretKey"];
            var key = System.Text.Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]);

            var key1 = System.Text.Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"]);
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,  // Replace with your issuer validation logic
                ValidateAudience = false,  // Replace with your audience validation logic
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            try
            {
                ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                JwtSecurityToken jwtToken = (JwtSecurityToken)validatedToken;

                // Access claims from the JWT token
                foreach (var claim in claimsPrincipal.Claims)
                {
                    Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
                    // You can process or store claims as needed
                }
            }
            catch (Exception ex)
            {
                // Handle token validation errors
                Console.WriteLine($"Token validation error: {ex.Message}");
            }
        }

    }
}
