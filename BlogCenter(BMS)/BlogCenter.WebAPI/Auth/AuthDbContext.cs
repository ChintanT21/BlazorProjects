using BlogCenter.WebAPI.Repositories.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogCenter.WebAPI.Auth
{
    public class AuthDbContext(DbContextOptions options) : IdentityDbContext<ApplicationUser>(options)
    {
    }
}
