using BMS.Client.Authentication;
using BMS.Client.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace BMS.Client.Components.Pages.Login
{
    public partial class Login
    {
        [Inject]
        private HttpClient _httpClient { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private IConfiguration? _configuration;
        protected LoginDto loginDto = new();
        string unauthorizedMessage = string.Empty;
        JwtSecurityTokenHandler tokenHandler;
        TokenValidationParameters validationParameters;
        string token;

        protected override async Task OnInitializedAsync()
        {
            _configuration = Configuration;
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                token = await FetchTokenFromLocalStorage();
            }
        }
        public async Task AuthenticateUser()
        {
            if (loginDto is null)
            {
                throw new ArgumentNullException(nameof(loginDto));
            }

            var response = await _httpClient.PostAsJsonAsync("https://localhost:7185/api/Auth/login", loginDto);
            var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
            var token = loginResponse.token;
            if (token != null)
            {
                await TokenService(token);
                await StoreTokenToLocalStorage(token);
                SetCookie(token);
                return;
            }
            unauthorizedMessage = "No valid parameters.";
        }
        private async void SetCookie(string token)
        {
            await JSRuntime.InvokeVoidAsync("setCookie", "authToken1", token);
        }
        protected async Task StoreTokenToLocalStorage(string token)
        {
            await JSRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", token);
        }
        protected async Task<string> FetchTokenFromLocalStorage()
        {
            var storedToken = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
            return storedToken;
        }

        public async Task TokenService(string token)
        {
            if (_configuration == null)
            {
                throw new InvalidOperationException("Configuration has not been initialized.");
            }
            tokenHandler = new JwtSecurityTokenHandler();
            var key = _configuration["JwtSettings:SecretKey"];
            var keyBytes = System.Text.Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]);
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            await TokenClaimRedirection(token, tokenHandler, validationParameters);
        }

        public async Task TokenClaimRedirection(string token, JwtSecurityTokenHandler tokenHandler, TokenValidationParameters validationParameters)
        {

            try
            {
                tokenHandler = new JwtSecurityTokenHandler();
                validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "https://id.nickchapsas.com",
                    ValidAudience = "https://movies.nickchapsas.com",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqustuvwxyz")),
                };
                var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
                var identity = new ClaimsIdentity(claimsPrincipal.Claims, "jwt");
                var user = new ClaimsPrincipal(identity);
                if (AuthStateProvider is CustomAuthenticationStateProvider customAuthStateProvider)
                {
                    customAuthStateProvider.NotifyUserAuthentication(user);
                }
                NavigationManager.NavigateTo("/adminDashboard");
                var authProvider = (CustomAuthenticationStateProvider)AuthStateProvider;
                authProvider.NotifyUserAuthentication(user);



            }
            catch (Exception ex)
            {
                Console.WriteLine($"Token validation error: {ex.Message}");
            }
        }

    }
}
