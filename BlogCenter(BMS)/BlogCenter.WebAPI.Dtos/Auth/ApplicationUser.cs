using Microsoft.AspNetCore.Identity;

namespace BlogCenter.WebAPI.Repositories.Auth
{
    public class ApplicationUser : IdentityUser
    {
       public string? Name { get; set; }
    }
}