using BlogCenter.WebAPI.Dtos;
using BlogCenter.WebAPI.Dtos.Mapper;
using BlogCenter.WebAPI.Dtos.RequestDto;
using BlogCenter.WebAPI.Repositories.Base;
using BlogCenter.WebAPI.Repositories;
using BlogCenter.WebAPI.Repositories.Utils;
using System.Linq.Expressions;
using System.Net;
using static BlogCenter.WebAPI.Dtos.Enums.Enums;
using static BlogCenter.WebAPI.Dtos.Mapper.UserMapper;
using static BlogCenter.WebAPI.Dtos.ResponceDto.UserDto;
using BlogCenter.WebAPI.Repositories.User;

namespace BlogCenter.WebAPI.Services.User
{
    public class UserService(IUserRepository _userRepository) : BaseService<Models.Models.User>(_userRepository), IUserService
    {
        Models.Models.User user = new();
        public async Task<GetUserDto> AddUser(long currentUserId, AddUserDto addUserDto)
        {
            GetUserDto getUserDto = new();
            user = addUserDto.MapToUser();
            user.CreatedBy = currentUserId;
            user = await AddAsync(user);
            return await GetUserByUserId(user.Id); ;
        }

        public async Task<bool> DeleteUser(long currentUserId, long userId)
        {
            Expression<Func<Models.Models.User, bool>> where = b => b.Id == userId;
            user = await GetOneAsync(where, null, null);
            user.UpdatedBy = currentUserId;
            user.UpdatedDate = DateTime.Now;
            user.Status = (short)UserStatus.Deleted;
            user = await UpdateAsync(user);
            if (user != null)
            {
                return true;
            }
            return false;
        }

        public async Task<List<Models.Models.User>> GetAll()
        {
            Expression<Func<Models.Models.User, bool>> where = b => b.Status != (short)UserStatus.Deleted;
            Expression<Func<Models.Models.User, object>> includeRole = b => b.Role;
            List<Models.Models.User> result = await GetAllAsync(where,null, includeRole);
            return result;
        }

        public async Task<GetUserDto> GetUserByUserId(long userId)
        {
            Expression<Func<Models.Models.User, bool>> where = b => b.Id == userId;
            Expression<Func<Models.Models.User, object>> includeRole = b => b.Role;
            user = await GetOneAsync(where, null, includeRole);
            return user.MapToGetUserDto();
        }

        public async Task<ApiPaginationResponse<GetUserDto>> GetUsersPageWise(string searchString, string searchTable, string sortString, int page, int pageSize, long userId)
        {
            Expression<Func<Models.Models.User, bool>> where = b => true;
            if (userId != 0)
            {
                where = where.And(b => b.CreatedBy == userId);
            }
            Expression<Func<Models.Models.User, object>> includeRole = b => b.Role;
            // Apply search string filter to the where expression
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                if (!string.IsNullOrWhiteSpace(searchTable))
                {
                    switch (searchTable.ToLower())
                    {
                        case "role":
                            if (Int32.TryParse(searchString, out int roleId) && roleId != 0)
                            {
                                where = where.And(b => b.RoleId == roleId);
                            }
                            break;
                        case "name":
                            where = where.And(b => b.FirstName.ToLower().Contains(searchString.ToLower().Trim()) || b.LastName.ToLower().Contains(searchString.ToLower().Trim()));
                            break;
                        case "status":
                            if (Int16.TryParse(searchString, out short statusId) && statusId != 0)
                            {
                                where = where.And(b => b.Status == statusId);
                            }
                            break;
                        default:
                            where = where.And(b => b.FirstName.ToLower().Contains(searchString.ToLower().Trim()) || b.LastName.ToLower().Contains(searchString.ToLower().Trim()));
                            break;
                    }
                }
               
            }

            // Apply sorting
            Func<IQueryable<Models.Models.User>, IOrderedQueryable<Models.Models.User>>? orderBy = null;

            sortString = "CreatedDate,UpdatedDate";
            if (!string.IsNullOrWhiteSpace(sortString))
            {
                var sortFields = sortString.Split(',');
                orderBy = query =>
                {
                    IOrderedQueryable<Models.Models.User> orderedQuery = null!;
                    foreach (var field in sortFields)
                    {
                        string sortOrder = "ascending";
                        var sortField = field.Trim();
                        if (sortField.StartsWith("-"))
                        {
                            sortField = sortField.TrimStart('-');
                            sortOrder = "descending";
                        }

                        var parameter = Expression.Parameter(typeof(Models.Models.User), "b");
                        var property = Expression.Property(parameter, sortField);
                        var lambda = Expression.Lambda(property, parameter);

                        if (orderedQuery == null)
                        {
                            orderedQuery = sortOrder == "ascending"
                                ? Queryable.OrderBy(query, (dynamic)lambda)
                                : Queryable.OrderByDescending(query, (dynamic)lambda);
                        }
                        else
                        {
                            orderedQuery = sortOrder == "ascending"
                                ? Queryable.ThenBy(orderedQuery, (dynamic)lambda)
                                : Queryable.ThenByDescending(orderedQuery, (dynamic)lambda);
                        }
                    }
                    return orderedQuery ?? query.OrderBy(x => 0);
                };
            }

            PagedItemResult<Models.Models.User> pagedBlogData = await GetAllWithPaginationAsync(page, pageSize, where, new[] { includeRole }, orderBy);
            ApiPaginationResponse<GetUserDto> apiPaginationResponse = new()
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Result = pagedBlogData.Items.MapToGetUserDtoList(),
                TotalPages = pagedBlogData.TotalPages,
                TotalCount = pagedBlogData.TotalCount,
            };
            return apiPaginationResponse;
        }

        public async Task<bool> IsUserExits(string email)
        {
            Expression<Func<Models.Models.User, bool>> where = b => b.Email.ToLower().Trim() == email.ToLower().Trim();
            Expression<Func<Models.Models.User, object>> includeRole = b => b.Role;
            user = await GetOneAsync(where, null, includeRole);
            if (user != null)
            {
                return true;
            }
            return false;
        }

        public async Task<GetUserDto> UpdateUser(long currentUserId, UpdateUserDto updateUserDto)
        {
            long userId = updateUserDto.UserId;
            Expression<Func<Models.Models.User, bool>> where = b => b.Id == userId;
            Expression<Func<Models.Models.User, object>> includeRole = b => b.Role;
            user = await GetOneAsync(where, null, includeRole);
            if (user == null)
            {
                return new GetUserDto();
            }
            MapToUser(user, updateUserDto);
            user.UpdatedBy = currentUserId;
            user.UpdatedDate = DateTime.Now;
            user = await UpdateAsync(user);
            return await GetUserByUserId(user.Id);
        }
    }
}
