using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BMS.Server.Auth
{
    public class AuthDbContext(DbContextOptions options) : IdentityDbContext<ApplicationUser>(options)
    {
    }
}
 