using BlogCenter.WebAPI.Dtos.RequestDto;
using BlogCenter.WebAPI.Dtos.ResponceDto;
using BlogCenter.WebAPI.Repositories.Blog;
using BMS.Server.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCenter.WebAPI.Services.Blog
{
    public class BlogService(IBlogRepository _blogRepository) : IBlogService
    {
        public Task<ApiResponse> AddBlogAsync(BlogDto blogDto)
        {
            return _blogRepository.AddBlogAsync(blogDto);
        }

        public Task<ApiResponse> DeleteBlogById(long id)
        {
            return _blogRepository.DeleteBlogById(id);
        }

        public Task<ApiResponse> GetBlogById(long id)
        {
            return _blogRepository.GetBlogById(id);
        }

        public async Task<ApiPaginationResponse> GetBlogsPageWise(string? searchString, string? sortString,int page, int pageSize)
        {
            ApiPaginationResponse apiPaginationResponse = await _blogRepository.GetBlogsWithPaginationFilteringAndSortingAsync(searchString,sortString,page, pageSize);
            return apiPaginationResponse;



        }
    }
}
