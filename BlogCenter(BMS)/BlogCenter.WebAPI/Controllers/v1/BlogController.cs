using BlogCenter.WebAPI.Dtos.ResponceDto;
using BlogCenter.WebAPI.Services.Blog;
using BMS.Server.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogCenter.WebAPI.Controllers.v1
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController(IBlogService _blogService) : ControllerBase
    {
        [HttpGet("GetBlogs")]
        public async Task<ActionResult<ApiPaginationResponse>> GetBlogs(string? searchString="", string? sortString="",int page =1,int pageSize=5)
        {
            ApiPaginationResponse apiPaginationResponse = await _blogService.GetBlogsPageWise(searchString, sortString,page, pageSize);
            return Ok(apiPaginationResponse);
        }

        [HttpGet("GetBlogByID/{id:long}")]
        public async Task<ActionResult<ApiResponse>> GetBlogById(long id = 1)
        {
            try
            {
                ApiResponse apiResponse = await _blogService.GetBlogById(id);
                if (apiResponse == null)
                {
                    return NotFound();
                }

                return Ok(apiResponse);
            }
            catch (Exception ex) { return StatusCode(500, $"Internal server error: {ex.Message}"); }
        }

        [HttpDelete("DeleteBlogById/{id:long}")]
        public async Task<ActionResult<ApiResponse>> DeleteBlogById(long id)
        {
            try
            {
                ApiResponse apiResponse = await _blogService.DeleteBlogById(id);
                return Ok(apiResponse);
            }
            catch (Exception ex) { return BadRequest(); };

        }

        [HttpPost("AddBlog")]
        public async Task<ActionResult<ApiResponse>> AddBlog(BlogDto blogDto)
        {
            try
            {
                ApiResponse apiResponse = await _blogService.AddBlogAsync(blogDto);
                return Ok(apiResponse);
            }
            catch (Exception ex) { return BadRequest(); };
        }
    }
}
