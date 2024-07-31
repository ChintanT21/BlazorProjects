using System.Security.Claims;

namespace BlogCenter.Blazor.Services.Role
{
    public interface IUserRoleService
    {
        Task<bool> IsUserInRoleAsync(ClaimsPrincipal user, string role);
    }
}
