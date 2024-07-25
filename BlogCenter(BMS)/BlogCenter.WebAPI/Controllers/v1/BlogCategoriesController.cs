using BlogCenter.WebAPI.Dtos;
using BlogCenter.WebAPI.Models.Models;
using BlogCenter.WebAPI.Services.BlogCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogCenter.WebAPI.Controllers.v1
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogCategoriesController(IBlogCategoryService _blogCategoryService) : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<ApiResponse> GetAllCategory()
        {
            ApiResponse apiResponse = new();
            try
            {
                List<BlogsCategory> blogCategories = await _blogCategoryService.GetAllCategory();
                apiResponse = new()
                {
                    Result = blogCategories,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    IsSuccess = true
                };
                return apiResponse;
            }
            catch (Exception e)
            {
                return apiResponse = new()
                {
                    StatusCode = System.Net.HttpStatusCode.NoContent,
                    IsSuccess = false
                };
            }
        }

        [HttpGet("{id:long}")]
        public async Task<ApiResponse> GetCategoriesByBlogId(long id)
        {
            ApiResponse apiResponse = new();
            try
            {
                List<BlogsCategory> blogCategories = await _blogCategoryService.GetByBlogId(id);
                apiResponse = new()
                {
                    Result = blogCategories,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    IsSuccess = true
                };
                return apiResponse;
            }
            catch (Exception e)
            {
                return apiResponse = new()
                {
                    StatusCode = System.Net.HttpStatusCode.NoContent,
                    IsSuccess = false
                };
            }
        }

    }
}
