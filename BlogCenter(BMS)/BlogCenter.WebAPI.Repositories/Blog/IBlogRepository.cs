using BlogCenter.WebAPI.Dtos.RequestDto;
using BlogCenter.WebAPI.Dtos.ResponceDto;
using BMS.Server.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCenter.WebAPI.Repositories.Blog
{
    public interface IBlogRepository
    {
        Task<ApiResponse> AddBlogAsync(BlogDto blogDto, long? userId);
        Task<ApiResponse> DeleteBlogById(long id,long? userId);
        Task<Models.Models.Blog> GetBlogById(long id);
        Task<List<Models.Models.Blog>> GetBlogsByUserId(string? searchString, string? sortString, long userId);
        Task<ApiPaginationResponse> GetBlogsWithPaginationFilteringAndSortingAsync(string searchString, string searchTable, string sortString, int page, int pageSize, long userId);
        void UpdateBlog(Models.Models.Blog blog);
    }
}
