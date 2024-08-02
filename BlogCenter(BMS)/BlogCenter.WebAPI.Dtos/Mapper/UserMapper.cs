using BlogCenter.WebAPI.Dtos.RequestDto;
using BlogCenter.WebAPI.Dtos.ResponceDto;
using BlogCenter.WebAPI.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BlogCenter.WebAPI.Dtos.Enums.Enums;
using static BlogCenter.WebAPI.Dtos.ResponceDto.UserDto;

namespace BlogCenter.WebAPI.Dtos.Mapper
{
    public static class UserMapper
    {
        public static User MapToUser(this AddUserDto dto)
        {
            return new User
            {
                RoleId=dto.RoleId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                ProfileName = dto.ProfileName,
                Password = dto.Password, // Ensure you hash the password before saving it
                CreatedBy = dto.CreatedBy,
                Status = dto.Status,
                CreatedDate = DateTime.Now, // Assuming you set the creation date

            };
        }
        public static void MapToUser(User user, UpdateUserDto dto)
        {
            user.RoleId = dto.RoleId;
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Email = dto.Email;
            user.ProfileName = dto.ProfileName;
            if (!string.IsNullOrEmpty(dto.Password))
            {
                user.Password = dto.Password; // Ensure you hash the password before saving it
            }
            user.UpdatedDate = DateTime.Now; // Assuming you set the update date
            user.Status = dto.Status;
        }
        public static GetUserDto MapToGetUserDto(this User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return new GetUserDto
            {
                Id = user.Id,
                RoleId = user.RoleId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                RoleName=user.Role.Name,
                ProfileName = user.ProfileName,
                CreatedDate = user.CreatedDate,
                UpdatedDate = user.UpdatedDate,
                CreatedBy = user.CreatedBy,
                UpdatedBy = user.UpdatedBy,
                Status = user.Status,
                StatusName = Enum.IsDefined(typeof(UserStatus), user.Status) ? (UserStatus)user.Status : throw new ArgumentOutOfRangeException(nameof(user.Status), "Invalid status value"),
            };
        }
        public static List<GetUserDto> MapToGetUserDtoList(this IEnumerable<User> users)
        {
            if (users == null)
            {
                throw new ArgumentNullException(nameof(users));
            }
            return users.Select(user => user.MapToGetUserDto()).ToList();
        }
        public static UpdateUserDto ToUpdateUserDto(this GetUserDto getUserDto)
        {
            return new UpdateUserDto
            {
                UserId = getUserDto.Id,
                RoleId = getUserDto.RoleId,
                FirstName = getUserDto.FirstName,
                LastName = getUserDto.LastName,
                Email = getUserDto.Email,
                ProfileName = getUserDto.ProfileName,
                Password = getUserDto.Password,
                Status = getUserDto.Status
            };
        }

    }
}
