using BlogCenter.WebAPI.Dtos;
using BlogCenter.WebAPI.Dtos.ResponceDto;
using BlogCenter.WebAPI.Filters;
using BlogCenter.WebAPI.Models.Models;
using BlogCenter.WebAPI.Services.Blog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BlogCenter.WebAPI.Dtos.RequestDto.GetBlogDto;

namespace BlogCenter.WebAPI.Controllers.v1
{
    [ApiController]
    [TypeFilter(typeof(TokenIdentifierFilter))]
    [Route("api/[controller]")]
    public class BlogsController(IBlogService _blogService) : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<ApiPaginationResponse<GetBlog>>> GetBlogs(int page = 1, int pageSize = 5, string searchString = "", string sortString = "", string searchTable = "", string sortTable = "", long userId = 0)
        {
            ApiPaginationResponse<GetBlog> apiPaginationResponse = await _blogService.GetBlogsPageWise(searchString, searchTable, sortString, page, pageSize, userId);
            return Ok(apiPaginationResponse);
        }

        [HttpGet("userid/{userId:long}")]
        public async Task<IActionResult> GetBlogByUserId(long userId, string? searchString = "", string? sortString = "")
        {
            ApiResponse apiResponse = await _blogService.GetBlogs(searchString, sortString, userId);
            return Ok(apiResponse);
        }

        [HttpGet("{blogId:long}")]
        public async Task<IActionResult> GetBlogById(long blogId = 0)
        {
            Response<GetBlog> responce = new();
            if (blogId==0)
            {
                return NotFound();
            }
            try
            {
                GetBlog getBlog = await _blogService.GetBlogById(blogId);
                if (getBlog == null)
                {
                    return NotFound();
                }

                responce = new()
                {
                    IsSuccess = true,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    ResultOne = getBlog
                };
                return Ok(responce);
            }
            catch (Exception ex) { return StatusCode(500, $"Internal server error: {ex.Message}"); }
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteBlogById(long id=0)
        {
            if(id == 0)
            {
                return NotFound();
            }
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
        public async Task<IActionResult> AddBlog(AddBlogDto blogDto)//userid whom add blog
        {
            if (blogDto is null)
            {
                return NoContent();
            }
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

        [HttpPost("statusId/{statusId:int}")]
        public async Task<IActionResult> ChangeStatus(long blogId=0, int statusId = 0)
        {
            if (blogId==0 || statusId == 0)
            {
                return NoContent();
            }
            try
            {
                if (HttpContext.Items.TryGetValue("TokenDto", out var tokenDtoObj) && tokenDtoObj is TokenDto tokenDto)
                {
                    Response<Blog> apiResponse = await _blogService.ChangeStatus(blogId, statusId, tokenDto.Id);
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
                    ApiResponse apiResponse = await _blogService.UpdateBlog(blogId, blogDto, tokenDto.Id);
                    return Ok(apiResponse);
                }
                return Unauthorized();
            }
            catch (Exception ex) { return BadRequest(); }
        }
    }
}
