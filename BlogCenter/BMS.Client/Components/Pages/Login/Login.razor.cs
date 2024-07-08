using BMS.Client.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
namespace BMS.Client.Components.Pages.Login
{
    public partial class Login
    {
        [Inject]
        HttpClient? Client { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private IConfiguration? _configuration;

        protected override void OnInitialized()
        {
            _configuration = Configuration;
        }

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
            if (_configuration == null)
            {
                throw new InvalidOperationException("Configuration has not been initialized.");
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key2 = _configuration["JwtSettings:SecretKey"];
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
                    if (claim.Type == ("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name") && claim.Value == ("Admin"))
                    {
                        NavigationManager.NavigateTo("/dashboard");
                    }
                    else
                    {
                        unauthorizedMessage = "NO valid Parameters";
                    }
                    Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Token validation error: {ex.Message}");
            }
        }

    }
}
