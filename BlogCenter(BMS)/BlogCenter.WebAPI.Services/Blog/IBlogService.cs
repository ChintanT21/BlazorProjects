

using BlogCenter.WebAPI.Dtos.RequestDto;
using BlogCenter.WebAPI.Dtos.ResponceDto;
using BlogCenter.WebAPI.Models.Models;
using BMS.Server.ViewModels;

namespace BlogCenter.WebAPI.Services.Blog
{
    public interface IBlogService
    {
        Task<ApiResponse> AddBlogAsync(BlogDto blogDto, long? userId);
        Task<ApiResponse> DeleteBlogById(long id,long? userId);
        Task<ApiResponse> GetBlogById(long id);
        Task<ApiResponse> GetBlogs(string? searchString, string? sortString, long userId);
        Task<ApiPaginationResponse> GetBlogsPageWise(string searchString, string searchTable, string sortString, int page,int pageSize,long userId);
        Task<ApiResponse> UpdateBlog(int blogId, BlogDto blogDto, long? id);
    }
}
