using BlogCenter.WebAPI.Dtos;
using BlogCenter.WebAPI.Dtos.ResponceDto;
using BlogCenter.WebAPI.Dtos.Mapper;
using BlogCenter.WebAPI.Repositories.Blog;
using BlogCenter.WebAPI.Repositories.BlogCategory;
using BlogCenter.WebAPI.Services.Auth;
using System.Net;
using BlogCenter.WebAPI.Models.Models;
using static BlogCenter.WebAPI.Dtos.RequestDto.GetBlogDto;

namespace BlogCenter.WebAPI.Services.Blog
{
    public class BlogService(IBlogRepository _blogRepository, IAuthService _authService, IBlogCategoryRepository _blogCategoryRepository) : IBlogService
    {
        public async Task<ApiResponse> AddBlogAsync(AddBlogDto blogDto, long userId)
        {
            ApiResponse apiResponse = new();
            Models.Models.Blog addedblog = await _blogRepository.AddBlogAsync(blogDto.AddDtoToBlog(), userId);
            //object resultObject = apiResponse.Result;
            //if (resultObject is Models.Models.Blog blog)
            //{
            //    blogId = blog.Id;
            //}
            long blogId = addedblog.Id;
            if (blogId != 0)
            {
                var obj = _blogCategoryRepository.AddBlogCategoryAsync(blogId, blogDto.Categories, userId);

            }
            else
            {
                return apiResponse = new()
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.NotFound,
                    ErrorMessages = ["Server Error"]
                };
            }
            return apiResponse = new()
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Result = addedblog
            };
        }

        public async Task<ApiResponse> DeleteBlogById(long id, long? userId)
        {
            return await _blogRepository.DeleteBlogById(id, userId);
        }

        public async Task<GetBlog> GetBlogById(long id)
        {
            GetBlog getBlog = new();
            Models.Models.Blog blog = await _blogRepository.GetBlogById(id);
            blog.BlogsCategories = await _blogCategoryRepository.GetByBlogId(id);
            if (blog != null)
            {
                return blog.ToGetBlogDto();
            }
            return getBlog;
        }

        public async Task<ApiPaginationResponse<GetBlog>> GetBlogsPageWise(string searchString, string searchTable, string sortString, int page, int pageSize, long userId)
        {
            ApiPaginationResponse<GetBlog> apiPaginationResponse = await _blogRepository.GetBlogsWithPaginationFilteringAndSortingAsync(searchString, searchTable, sortString, page, pageSize, userId);
            return apiPaginationResponse;
        }

        public async Task<ApiResponse> UpdateBlog(int blogId, BlogDto blogDto, long? id)
        {
            ApiResponse apiResponse = new ApiResponse();
            Models.Models.Blog blog = await _blogRepository.GetBlogById(blogId);
            blog.Content = blogDto.Content ?? blog.Content;
            blog.Title = blogDto.Title ?? blog.Title;
            blog.UpdatedBy = id;
            blog.UpdatedDate = blogDto.UpdatedDate;
            blog.Status = blogDto.Status ?? blog.Status;
            _blogRepository.UpdateBlog(blog);
            if (blogDto.Categories != null)
            {
                await _blogCategoryRepository.UpdateBlogCategory(blogId, blogDto.Categories, id);
            }
            if (blog != null)
            {
                return apiResponse = new()
                {
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                    Result = blog
                };
            }
            return apiResponse = new()
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.NoContent,
            };
        }

        public async Task<ApiResponse> GetBlogs(string? searchString, string? sortString, long userId)
        {
            ApiResponse apiResponse = new();
            List<Models.Models.Blog> blogList = await _blogRepository.GetBlogsByUserId(searchString, sortString, userId);
            if (blogList.Count == 0)
            {
                return apiResponse = new()
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.NoContent,
                };
            }
            return apiResponse = new()
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Result = blogList
            };
        }

        public async Task<Response<Models.Models.Blog>> ChangeStatus(long blogId, int statusId, long id)
        {
            Response<Models.Models.Blog> apiResponse = new();
            Models.Models.Blog blog = await _blogRepository.GetBlogById(blogId);
            blog.Status = (short)statusId;
            blog.UpdatedBy = id;
            blog.UpdatedDate = DateTime.Now;
            await  _blogRepository.UpdateBlog(blog);
            if (blog != null)
            {
                return apiResponse = new()
                {
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                    ResultOne = blog
                };
            }
            return apiResponse = new()
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.NoContent,
            };

        }
    }
}
