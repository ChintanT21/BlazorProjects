using BMS.Client.Authentication;
using BMS.Client.Dtos;
using BMS.Server.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace BlogCenter.Blazor.Components.Pages.Authentication.Login
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

        public async Task AuthenticateUser()
        {
            if (loginDto is null)
            {
                throw new ArgumentNullException(nameof(loginDto));
            }
            var response = await _httpClient.PostAsJsonAsync("/api/Auth/login", loginDto);
            var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
            string token = loginResponse.token;
            if (token != null)
            {
                SetCookie(token, 59);
            }
            HttpResponseMessage userclaims = await _httpClient.PostAsJsonAsync("/api/Auth/tokenvalidator?token=" + token, token);
            ApiResponse apiResponse = await userclaims.Content.ReadFromJsonAsync<ApiResponse>();
            await TokenClaimRedirection(token, tokenHandler, validationParameters, apiResponse);
        }
        private void SetCookie(string token, int? expireTime)
        {
            JSRuntime.InvokeVoidAsync("setCookie", "authToken", token);
            //var option = new CookieOptions();

            //if (expireTime.HasValue)
            //{
            //    option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            //}
            //else
            //{
            //    option.Expires = DateTime.Now.AddMilliseconds(10);
            //}
            //httpContextAccessor.HttpContext.Response.Cookies.Append("authToken", token, option);
        }
        public class CustomClaim
        {
            public string Type { get; set; }
            public string Value { get; set; }
        }
        public async Task TokenClaimRedirection(string token, JwtSecurityTokenHandler tokenHandler, TokenValidationParameters validationParameters, ApiResponse apiResponse)
        {
            try
            {

                string jsonResponse = apiResponse.Result.ToString();
                var claims = JsonSerializer.Deserialize<List<string>>(jsonResponse)
                     .Select(c =>
                     {
                         var parts = c.Split(", ");
                         return new CustomClaim
                         {
                             Type = parts[0].Substring("Type: ".Length),
                             Value = parts[1].Substring("Value: ".Length)
                         };
                     }).ToList();

                var ClaimTypeEmail = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
                var ClaimTypeId = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
                var ClaimTypeName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
                var ClaimTypeRole = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";

                string userEmail = claims.FirstOrDefault(c => c.Type == ClaimTypeEmail).Value;
                string userId = claims.FirstOrDefault(c => c.Type == ClaimTypeId).Value;
                string userName = claims.FirstOrDefault(c => c.Type == ClaimTypeName).Value;
                string userRole = claims.FirstOrDefault(c => c.Type == ClaimTypeRole).Value;

                var identity = new ClaimsIdentity(new[]
                       {
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.Email,userEmail ),
            new Claim(ClaimTypes.NameIdentifier,userEmail),
            new Claim(ClaimTypes.Role,userRole)

        });

                var user = new ClaimsPrincipal(identity);
                var authProvider = (CustomAuthenticationStateProvider)AuthStateProvider;
                authProvider.NotifyUserAuthentication(user);
                if (user.IsInRole("admin"))
                {
                    NavigationManager.NavigateTo("/adminDashboard");
                }
                else if (user.IsInRole("user"))
                {
                    NavigationManager.NavigateTo("/userDashboard");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Token validation error: {ex.Message}");
            }
        }

    }
}
