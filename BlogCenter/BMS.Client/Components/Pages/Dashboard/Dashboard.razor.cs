using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BMS.Client.Components.Pages.Dashboard
{
    public partial class Dashboard
    {
        [Inject]
        protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        private string UserName { get; set; }
        private string UserRole { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            UserName = user.Identity?.Name ?? "inValid";
            UserRole = user.FindFirst(ClaimTypes.Role)?.Value ?? "Unknown";
        }
    }
}
