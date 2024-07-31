using BlogCenter.WebAPI.Dtos;
using BlogCenter.WebAPI.Dtos.RequestDto;
using BlogCenter.WebAPI.Models.Models;
using BlogCenter.WebAPI.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCenter.WebAPI.Services.User
{
    public interface IUserService : IBaseService<Models.Models.User>
    {
        Task<GetUserDto> AddUser(long currentUserId, Dtos.ResponceDto.UserDto.AddUserDto addUserDto);
        Task<bool> DeleteUser(long currentUserId, long userId);
        Task<List<Models.Models.User>> GetAll();
        Task<GetUserDto> GetUserByUserId(long userId);
        Task<ApiPaginationResponse<GetUserDto>> GetUsersPageWise(string searchString, string searchTable, string sortString, int page, int pageSize, long userId);
        Task<bool> IsUserExits(string email);
        Task<GetUserDto> UpdateUser(long currentUserId, Dtos.ResponceDto.UserDto.UpdateUserDto updateUserDto);
    }
}
