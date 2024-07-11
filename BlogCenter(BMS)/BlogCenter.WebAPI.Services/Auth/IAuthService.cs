using BMS.Client.Dtos;
using BMS.Server.ViewModels;
using static BMS.Server.ViewModels.ServiceResponses;

namespace BlogCenter.WebAPI.Services.Auth
{
    public interface IAuthService
    {
        Task<GeneralResponse> CreateAccount(UserDto UserDto);
        Task<LoginResponse> LoginAccount(LoginDto loginDTO);
        Task<bool> isTokenValidate(string token);
        Task<ApiResponse> TokenValidator(string token);

    }
}
