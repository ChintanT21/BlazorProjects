

using BlogCenter.WebAPI.Dtos;
using BlogCenter.WebAPI.Dtos.ResponceDto;
using BlogCenter.WebAPI.Models.Models;
using static BlogCenter.WebAPI.Dtos.RequestDto.GetBlogDto;

namespace BlogCenter.WebAPI.Services.Blog
{
    public interface IBlogService
    {
        Task<ApiResponse> AddBlogAsync(AddBlogDto blogDto, long userId);
        Task<Response<Models.Models.Blog>> ChangeStatus(long blogId, int statusId, long id);
        Task<ApiResponse> DeleteBlogById(long id, long? userId);
        Task<GetBlog> GetBlogById(long id);
        Task<ApiResponse> GetBlogs(string? searchString, string? sortString, long userId);
        Task<ApiPaginationResponse<GetBlog>> GetBlogsPageWise(string searchString, string searchTable, string sortString, int page, int pageSize, long userId);
        Task<ApiResponse> UpdateBlog(int blogId, BlogDto blogDto, long? id);
    }
}
