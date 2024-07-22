using BlogCenter.WebAPI.Dtos.RequestDto;
using BlogCenter.WebAPI.Dtos.ResponceDto;
using BlogCenter.WebAPI.Filters;
using BlogCenter.WebAPI.Services.Blog;
using BMS.Server.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlogCenter.WebAPI.Controllers.v1
{
    [ApiController]
    [TypeFilter(typeof(TokenIdentifierFilter))]
    [Route("api/[controller]")]
    public class BlogController(IBlogService _blogService) : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetBlogs(int page = 1, int pageSize = 5,string searchString = "", string sortString = "",string searchTable="", string sortTable="",long userId=0)
        {
            ApiPaginationResponse apiPaginationResponse = await _blogService.GetBlogsPageWise(searchString, searchTable, sortString, page, pageSize, userId);
            return Ok(apiPaginationResponse);
        }

        [HttpGet("userid/{userId:long}")]
        public async Task<IActionResult> GetBlogByUserId(long userId,string? searchString = "", string? sortString = "")
        {
            ApiResponse apiResponse = await _blogService.GetBlogs(searchString, sortString, userId);
            return Ok(apiResponse);
        }

        [HttpGet("{blogId:long}")]
        public async Task<IActionResult> GetBlogById(long blogId = 1)
        {
            try
            {
                ApiResponse apiResponse = await _blogService.GetBlogById(blogId);
                if (apiResponse == null)
                {
                    return NotFound();
                }

                return Ok(apiResponse);
            }
            catch (Exception ex) { return StatusCode(500, $"Internal server error: {ex.Message}"); }
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteBlogById(long id)
        {
            try
            {
                if (HttpContext.Items.TryGetValue("TokenDto", out var tokenDtoObj) && tokenDtoObj is TokenDto tokenDto)
                {
                    long? userId = tokenDto.Id;
                    string token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");
                    ApiResponse apiResponse = await _blogService.DeleteBlogById(id, userId);
                    return Ok(apiResponse);
                }
                else { return Unauthorized(); }
            }
            catch (Exception ex) { return BadRequest(); };
        }

        [HttpPost]
        public async Task<IActionResult> AddBlog(BlogDto blogDto)//userid whom add blog
        {
            try
            {
                if (HttpContext.Items.TryGetValue("TokenDto", out var tokenDtoObj) && tokenDtoObj is TokenDto tokenDto)
                {
                    ApiResponse apiResponse = await _blogService.AddBlogAsync(blogDto, tokenDto.Id);
                    return Ok(apiResponse);
                }
                return Unauthorized();
            }
            catch (Exception ex) { return BadRequest(); };
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBlog(int blogId, BlogDto blogDto)
        {
            if (blogDto is null)
            {
               return NoContent();
            }

            try
            {
                if (HttpContext.Items.TryGetValue("TokenDto", out var tokenDtoObj) && tokenDtoObj is TokenDto tokenDto)
                {
                    ApiResponse apiResponse = await _blogService.UpdateBlog(blogId,blogDto, tokenDto.Id);
                    return Ok(apiResponse);
                }
                return Unauthorized();
            }
            catch (Exception ex) { return BadRequest();}
        }
    }
}
