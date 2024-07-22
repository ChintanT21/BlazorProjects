using BlogCenter.WebAPI.Dtos.RequestDto;
using BlogCenter.WebAPI.Dtos.ResponceDto;
using BlogCenter.WebAPI.Models.Models;
using BlogCenter.WebAPI.Repositories.Blog;
using BlogCenter.WebAPI.Repositories.BlogCategory;
using BlogCenter.WebAPI.Services.Auth;
using BMS.Server.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BlogCenter.WebAPI.Services.Blog
{
    public class BlogService(IBlogRepository _blogRepository, IAuthService _authService, IBlogCategoryRepository _blogCategoryRepository) : IBlogService
    {
        public async Task<ApiResponse> AddBlogAsync(BlogDto blogDto, long? userId)
        {
            long blogId = 0;
            ApiResponse apiResponse = await _blogRepository.AddBlogAsync(blogDto, userId);
            object resultObject = apiResponse.Result;
            if (resultObject is Models.Models.Blog blog)
            {
                blogId = blog.Id;
            }
            if (blogId != 0)
            {
                var obj = _blogCategoryRepository.AddBlogCategoryAsync(blogId, blogDto.Categories, userId);

            }
            return apiResponse;
        }

        public async Task<ApiResponse> DeleteBlogById(long id, long? userId)
        {
            return await _blogRepository.DeleteBlogById(id, userId);
        }

        public async Task<ApiResponse> GetBlogById(long id)
        {
            ApiResponse apiResponse = new ApiResponse();
            Models.Models.Blog blog = await _blogRepository.GetBlogById(id);
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

        public async  Task<ApiPaginationResponse> GetBlogsPageWise(string searchString, string searchTable, string sortString, int page, int pageSize, long userId)
        {
            ApiPaginationResponse apiPaginationResponse =  await _blogRepository.GetBlogsWithPaginationFilteringAndSortingAsync(searchString, searchTable, sortString, page, pageSize, userId);
            return  apiPaginationResponse;
        }

        public async Task<ApiResponse> UpdateBlog(int blogId, BlogDto blogDto, long? id)
        {
            ApiResponse apiResponse = new ApiResponse();
            Models.Models.Blog blog = await _blogRepository.GetBlogById(blogId);
            blog.Content = blogDto.Content ?? blog.Content;
            blog.Title = blogDto.Title ?? blog.Title;
            blog.UpdatedBy = id;
            blog.UpdatedDate = blogDto.UpdatedDate;
            _blogRepository.UpdateBlog(blog);
            if (blogDto.Categories != null)
            {
                _blogCategoryRepository.UpdateBlogCategory(blogId, blogDto.Categories, id);

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
            if (blogList.Count == 0) {
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
    }
}
