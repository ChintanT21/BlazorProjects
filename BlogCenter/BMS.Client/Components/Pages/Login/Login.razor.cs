using BMS.Client.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity.Data;
using System.Net.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace BMS.Client.Components.Pages.Login
{
    public partial class Login
    {
        [Inject]
        HttpClient? Client { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }


        protected LoginDto loginDto = new();  
        string responseBody = string.Empty;
        string unauthorizedMessage = string.Empty;

        public async Task AuthenticateUser()
        {
            if (loginDto is null)
            {
                throw new ArgumentNullException(nameof(loginDto));
            }
            var response = await Client.PostAsJsonAsync("https://localhost:7185/login", loginDto);
            if (response.IsSuccessStatusCode)
            {
                NavigationManager.NavigateTo("/dashboard");
                var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
                var token = loginResponse.accessToken;
                await SecurelyStoreToken(token);
                responseBody = await response.Content.ReadAsStringAsync();


                //var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
            }
            else
            {
                unauthorizedMessage = "Username or Password is invalid!";
            }
            
        }
        protected async Task SecurelyStoreToken(string token)
        {
            await localStorage.SetItemAsync("authToken", token);
        }

    }
}
