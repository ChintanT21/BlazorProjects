using BlogCenter.WebAPI.Dtos;
using BlogCenter.WebAPI.Dtos.Constant;
using BlogCenter.WebAPI.Dtos.Mapper;
using BlogCenter.WebAPI.Dtos.RequestDto;
using BlogCenter.WebAPI.Filters;
using BlogCenter.WebAPI.Services;
using BlogCenter.WebAPI.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BlogCenter.WebAPI.Dtos.ResponceDto.UserDto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogCenter.WebAPI.Controllers.v1
{
    [Route("api/[controller]")]
    [TypeFilter(typeof(TokenIdentifierFilter))]
    [ApiController]
    public class UsersController(IUserService _userService) : ControllerBase
    {
        // GET: api/<UsersController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            Response<GetUserDto> response = new();
            List<Models.Models.User> users = await _userService.GetAll();
            if (users != null)
            {
                response = new()
                {
                    IsSuccess = true,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    ResultList = users.MapToGetUserDtoList()
                };
                return Ok(response);
            }
            response = new()
            {
                IsSuccess = false,
                StatusCode = System.Net.HttpStatusCode.NoContent,
                ErrorMessages = ["No Content"]
            };
            return Ok(response);
        }

        [HttpGet("WithPaginantion")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllWithPagination(int page = 1, int pageSize = 5, string searchString = "", string sortString = "", string searchTable = "", long userId = 0)
        {

            ApiPaginationResponse<GetUserDto> apiPaginationResponse = await _userService.GetUsersPageWise(searchString, searchTable, sortString, page, pageSize, userId);
            if (apiPaginationResponse != null)
            {
                return Ok(apiPaginationResponse);
            }
            return NoContent();
        }

        [HttpGet("{userId:long}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetOne(long userId)
        {
            Response<GetUserDto> response = new();
            GetUserDto user = await _userService.GetUserByUserId(userId);
            if (user != null)
            {
                response = new()
                {
                    IsSuccess = true,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    ResultOne = user,
                };
                return Ok(response);
            }
            response = new()
            {
                IsSuccess = false,
                StatusCode = System.Net.HttpStatusCode.NoContent,
                ErrorMessages = ["No Content"]
            };
            return Ok(response);
        }


        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddUser(AddUserDto addUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            GetUserDto user = new();
            bool IsUserExits = false;
            string? currentUserId = HttpContext.Items["UserId"] as string;
            Response<GetUserDto> response = new();
            if (currentUserId != null)
            {
                IsUserExits = await _userService.IsUserExits(addUserDto.Email);
                if (!IsUserExits)
                {
                    user = await _userService.AddUser(Int64.Parse(currentUserId), addUserDto);
                }
            }
            if (user.Id != 0)
            {
                response = new()
                {
                    IsSuccess = true,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    ResultOne = user,
                };
                return Ok(response);
            }
            response = new()
            {
                IsSuccess = false,
                StatusCode = System.Net.HttpStatusCode.NoContent,
                ErrorMessages = IsUserExits == true ? [Constants.USER_EXISTS_ERROR] : [Constants.USER_NOTADD_ERROR],
            };
            return Ok(response);
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateUser(UpdateUserDto updateUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            GetUserDto user = new();
            bool IsUserExits = false;
            string? currentUserId = HttpContext.Items["UserId"] as string;
            Response<GetUserDto> response = new();
            if (currentUserId != null && updateUserDto.UserId!=0)
            {
                IsUserExits = await _userService.IsUserExits(updateUserDto.Email);
                if (!IsUserExits)
                {
                    user = await _userService.UpdateUser(Int64.Parse(currentUserId), updateUserDto);
                }
            }
            if (user != null)
            {
                response = new()
                {
                    IsSuccess = true,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    ResultOne = user,
                };
                return Ok(response);
            }
            response = new()
            {
                IsSuccess = false,
                StatusCode = System.Net.HttpStatusCode.NoContent,
                ErrorMessages = IsUserExits == true ? [Constants.USER_EXISTS_ERROR] : [Constants.USER_NOTADD_ERROR],
            };
            return Ok(response);
        }

        [HttpDelete("{userId:long}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUser(long userId = 0)
        {
            if (userId == 0)
            {
                return BadRequest(ModelState);
            }
            bool IsDeleted = false;
            GetUserDto user = new();
            string? currentUserId = HttpContext.Items["UserId"] as string;
            Response<GetUserDto> response = new();
            user = await _userService.GetUserByUserId(userId);
            if (user.Id != 0)
            {
                IsDeleted = await _userService.DeleteUser(Int64.Parse(currentUserId), userId);
            }

            if (IsDeleted)
            {
                response = new()
                {
                    IsSuccess = true,
                    StatusCode = System.Net.HttpStatusCode.OK,
                };
                return Ok(response);
            }
            response = new()
            {
                IsSuccess = false,
                StatusCode = System.Net.HttpStatusCode.NoContent,
                ErrorMessages = user.Id != 0 ? [Constants.USER_NOTFOUND_ERROR] : [Constants.USER_NOTDELETE_ERROR]
            };
            return Ok(response);
        }
    }
}
