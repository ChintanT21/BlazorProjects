using BlogCenter.WebAPI.Dtos;
using BlogCenter.WebAPI.Dtos.Constant;
using BlogCenter.WebAPI.Dtos.Mapper;
using BlogCenter.WebAPI.Dtos.RequestDto;
using BlogCenter.WebAPI.Filters;
using BlogCenter.WebAPI.Models.Models;
using BlogCenter.WebAPI.Services.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BlogCenter.WebAPI.Dtos.ResponceDto.CategoryDto;

namespace BlogCenter.WebAPI.Controllers.v1
{
    [ApiController]
    [TypeFilter(typeof(TokenIdentifierFilter))]
    [Route("api/[controller]")]
    public class CategoriesController(ICategoryService _categoryService) : ControllerBase
    {

        [HttpGet]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetAll()
        {
            Response<GetCategoryDto> response = new();
            List<Models.Models.Category> categories = await _categoryService.GetAll();
            if (categories != null)
            {
                response = new()
                {
                    IsSuccess = true,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    ResultList = categories.MapToGetCategoryDtoList()
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

        [HttpGet("{categoryId:int}")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetOne(int categoryId)
        {
            Response<GetCategoryDto> response = new();
            GetCategoryDto category = await _categoryService.GetCategoryById(categoryId);
            if (category != null)
            {
                response = new()
                {
                    IsSuccess = true,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    ResultOne = category,
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

            ApiPaginationResponse<GetCategoryDto> apiPaginationResponse = await _categoryService.GetCategoriesPageWise(searchString, searchTable, sortString, page, pageSize, userId);
            if (apiPaginationResponse != null)
            {
                return Ok(apiPaginationResponse);
            }
            return NoContent();
        }
        
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddCategory(AddCategoryDto addCategoryDto)
        {
            if (!ModelState.IsValid && addCategoryDto.Name == string.Empty)
            {
                return BadRequest(ModelState);
            }
            GetCategoryDto category = new();
            bool IsCategoryExits = false;
            string? userId = HttpContext.Items["UserId"] as string;
            string? userRole = HttpContext.Items["UserRole"] as string;
            Response<GetCategoryDto> response = new();
            if (userRole == "admin" && userId != null)
            {
                IsCategoryExits = await _categoryService.IsCategoryExits(addCategoryDto.Name);
                if (!IsCategoryExits)
                {
                    category = await _categoryService.AddCategory(Int64.Parse(userId), addCategoryDto);
                }
            }
            if (category.Id != 0)
            {
                response = new()
                {
                    IsSuccess = true,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    ResultOne = category,
                };
                return Ok(response);
            }
            response = new()
            {
                IsSuccess = false,
                StatusCode = System.Net.HttpStatusCode.NoContent,
                ErrorMessages = IsCategoryExits == true ? [Constants.CATEGORY_EXISTS_ERROR] : [Constants.CATEGORY_NOTADD_ERROR],
            };
            return Ok(response);
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            GetCategoryDto category = new();
            bool IsCategoryExits = false;
            string? userId = HttpContext.Items["UserId"] as string;
            string? userRole = HttpContext.Items["UserRole"] as string;
            Response<GetCategoryDto> response = new();
            if (userRole == "admin" && userId != null)
            {
                IsCategoryExits = await _categoryService.IsCategoryExits(updateCategoryDto.Name);
                if (!IsCategoryExits)
                {
                    category = await _categoryService.UpdateCategory(Int64.Parse(userId), updateCategoryDto);
                }
            }
            if (category != null)
            {
                response = new()
                {
                    IsSuccess = true,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    ResultOne = category,
                };
                return Ok(response);
            }
            response = new()
            {
                IsSuccess = false,
                StatusCode = System.Net.HttpStatusCode.NoContent,
                ErrorMessages = IsCategoryExits == true ? [Constants.CATEGORY_EXISTS_ERROR] : [Constants.CATEGORY_NOTADD_ERROR],
            };
            return Ok(response);
        }

        [HttpDelete("{categoryId:int}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteCategory(int categoryId = 0)
        {
            if (categoryId == 0)
            {
                return BadRequest(ModelState);
            }
            bool IsDeleted = false;
            List<long> blogs = new();
            string? userId = HttpContext.Items["UserId"] as string;
            string? userRole = HttpContext.Items["UserRole"] as string;
            Response<GetCategoryDto> response = new();
            blogs = await _categoryService.GetBlogsByCategoryId(categoryId);
            if (blogs.Count == 0)
            {
                IsDeleted = await _categoryService.DeleteCategory(Int64.Parse(userId), categoryId);
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
                ErrorMessages = blogs.Count != 0 ? [Constants.CATEGORY_BLOG_LINKED_ERROR] : [Constants.CATEGORY_NOTDELETE_ERROR]
            };
            return Ok(response);
        }

    }
}
