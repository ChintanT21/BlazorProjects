using BlogCenter.WebAPI.Models.Models;

namespace BlogCenter.WebAPI.Repositories.Auth
{
    public interface IAuthRepository
    {
        User GetUserByEmail(string email);
        Task<bool> CheckCredentialsByEmailAndPassword(string email, string password);
        Task<String> GetUserRoleByEmail(string email);
    }
}
