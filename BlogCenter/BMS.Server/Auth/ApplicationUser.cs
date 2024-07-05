using Microsoft.AspNetCore.Identity;

namespace BMS.Server.Auth
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
    }
}
