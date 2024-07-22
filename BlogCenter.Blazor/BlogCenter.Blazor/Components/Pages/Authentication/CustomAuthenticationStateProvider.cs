using BlogCenter.WebAPI.Models.Models;
using BMS.Server.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;


namespace BMS.Client.Authentication
{
    public class CustomAuthenticationStateProvider(IHttpContextAccessor httpContextAccessor, HttpClient httpClient) : AuthenticationStateProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext.User.Identity.IsAuthenticated)
            {
                return new AuthenticationState(httpContext.User);
            }

            var token = GetCookieValue();
            if (string.IsNullOrEmpty(token))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
            try
            {
                HttpResponseMessage userclaims = await httpClient.PostAsJsonAsync("/api/Auth/tokenvalidator?token=" + token, token);
                ApiResponse apiResponse = await userclaims.Content.ReadFromJsonAsync<ApiResponse>();
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

                string userEmail = claims.FirstOrDefault(c => c.Type == ClaimTypeEmail)?.Value ?? "Email not found";
                string userId = claims.FirstOrDefault(c => c.Type == ClaimTypeId)?.Value ?? "ID not found";
                string userName = claims.FirstOrDefault(c => c.Type == ClaimTypeName)?.Value ?? "Name not found";
                string userRole = claims.FirstOrDefault(c => c.Type == ClaimTypeRole)?.Value ?? "Role not found";
                if (userId!= "ID not found" && userRole!= "Role not found")
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                var identity = new ClaimsIdentity(new[]
                       {
                            new Claim(ClaimTypes.Name, userName),
                            new Claim(ClaimTypes.Email,userEmail ),
                            new Claim(ClaimTypes.NameIdentifier,userEmail),
                            new Claim(ClaimTypes.Role,userRole)

                        });

                var user = new ClaimsPrincipal(identity);
                //Task.Run(() => NavigateBasedOnRole(user));
                return new AuthenticationState(user);
            }
            catch
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "authToken")));
            }
        }


        private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }
        public void NotifyUserAuthentication(ClaimsPrincipal user)
        {
            var authState = Task.FromResult(new AuthenticationState(user));
            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyUserLogout()
        {
            var authState = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
            NotifyAuthenticationStateChanged(authState);
        }

        private string GetCookieValue()
        {
            return _httpContextAccessor.HttpContext.Request.Cookies["authToken"];
        }
        public class CustomClaim
        {
            public string Type { get; set; }
            public string Value { get; set; }
        }
        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        public void NotifyAuthState()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

    }
}