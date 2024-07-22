using BlogCenter.WebAPI.Models.Models;
using BlogCenter.WebAPI.Services.BlogCategory;
using BMS.Server.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BlogCenter.WebAPI.Controllers.v1
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogCategoryController(IBlogCategoryService _blogCategoryService) : Controller
    {

        [HttpGet("GetBlogCategoryByBlogId/{id:long}")]
        public async Task<ApiResponse> GetByBlogId(long id)
        {
                ApiResponse apiResponse= new ApiResponse();
            try
            {
                List<BlogsCategory> blogCategories = await _blogCategoryService.GetByBlogId(id);
                apiResponse = new()
                {
                    Result=blogCategories,
                    StatusCode=System.Net.HttpStatusCode.OK,
                    IsSuccess=true
                };
                return apiResponse;
            }
            catch(Exception e) { return apiResponse = new()
            {
                StatusCode = System.Net.HttpStatusCode.NoContent,
                IsSuccess = false
            }; }
        }
  
    }
}
