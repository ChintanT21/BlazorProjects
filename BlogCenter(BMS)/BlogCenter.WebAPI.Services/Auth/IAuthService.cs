using BlogCenter.WebAPI.Dtos;
using BlogCenter.WebAPI.Dtos.ResponceDto;
using static BlogCenter.WebAPI.Dtos.ServiceResponses;

namespace BlogCenter.WebAPI.Services.Auth
{
    public interface IAuthService
    {
        Task<GeneralResponse> CreateAccount(UserDto UserDto);
        Task<LoginResponse> LoginAccount(LoginDto loginDTO);
        Task<bool> isTokenValidate(string token);
        Task<ApiResponse> TokenValidator(string token);
        TokenDto GetUserDetailsByToken(string token);
    }
}
