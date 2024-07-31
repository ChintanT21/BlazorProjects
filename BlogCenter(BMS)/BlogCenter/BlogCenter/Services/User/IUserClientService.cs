using BlogCenter.WebAPI.Dtos.RequestDto;
using BlogCenter.WebAPI.Dtos.ResponceDto;
using BlogCenter.WebAPI.Dtos;
using static BlogCenter.WebAPI.Dtos.ResponceDto.UserDto;

namespace BlogCenter.Services.User
{
    public interface IUserClientService
    {
        Task<ApiPaginationResponse<GetUserDto>> GetUsersData(DataManipulationDto dto);
        Task<Dictionary<bool, string>> AddUser(AddUserDto dto);
        Task<Dictionary<bool, string>> UpdateUser(UpdateUserDto dto);
        Task<Dictionary<bool, string>> DeleteUser(long userId);
        Task<GetUserDto> GetUserById(long userId);
    }
}
