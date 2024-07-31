using BlogCenter.WebAPI.Models.Models;
namespace BlogCenter.WebAPI.Repositories.Auth
{

    public class AuthRepository(ApplicationDbContext _dbContext) : IAuthRepository
    {
        public async Task<bool> CheckCredentialsByEmailAndPassword(string email, string password)
        {
            bool user = _dbContext.Users.Any(x => x.Email == email && x.Password == password);
            return user;
        }

        public async Task<Models.Models.User> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users.FirstOrDefault(x => x.Email.ToLower() == email.ToLower());
            return user;
        }

        public async Task<string> GetUserRoleByEmail(string email)
        {
            var userRoleId = _dbContext.Users.FirstOrDefault(x => x.Email.ToLower() == email.ToLower()).RoleId;
            var userRole = _dbContext.Roles.FirstOrDefault(x => x.Id == userRoleId).Name;
            return userRole;
        }

    }
}

