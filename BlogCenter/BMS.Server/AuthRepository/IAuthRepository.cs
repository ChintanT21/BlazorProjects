using BMS.Server.ViewModels;
using static BMS.Server.ViewModels.ServiceResponses;

namespace BMS.Server.AuthRepository
{
    public interface IAuthRepository
    {
        Task<GeneralResponse> CreateAccount(UserDto UserDto);
        Task<LoginResponse> LoginAccount(LoginDto loginDTO);
        Task<bool> isTokenValidate(string token);
        Task<ApiResponse> TokenValidator(string token);
    }
}
