using BlogCenter.WebAPI.Repositories.Auth;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BlogCenter.Blazor.Services.Role
{
    public class UserRoleService : IUserRoleService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRoleService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> IsUserInRoleAsync(ClaimsPrincipal user, string role)
        {
            var identityUser = await _userManager.GetUserAsync(user);
            if (identityUser != null)
            {
                return await _userManager.IsInRoleAsync(identityUser, role);
            }
            return false;
        }
    }
}

