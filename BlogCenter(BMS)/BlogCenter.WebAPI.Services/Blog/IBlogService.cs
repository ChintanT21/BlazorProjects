

using BlogCenter.WebAPI.Dtos.RequestDto;
using BlogCenter.WebAPI.Dtos.ResponceDto;
using BMS.Server.ViewModels;

namespace BlogCenter.WebAPI.Services.Blog
{
    public interface IBlogService
    {
        Task<ApiResponse> AddBlogAsync(BlogDto blogDto);
        Task<ApiResponse> DeleteBlogById(long id);
        Task<ApiResponse> GetBlogById(long id);
        Task<ApiPaginationResponse> GetBlogsPageWise(string? searchString, string? sortString,int page, int pageSize);
    }
}
