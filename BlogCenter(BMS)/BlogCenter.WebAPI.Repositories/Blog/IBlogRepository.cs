using BlogCenter.WebAPI.Dtos;
using BlogCenter.WebAPI.Dtos.ResponceDto;
using BlogCenter.WebAPI.Models.Models;
using BlogCenter.WebAPI.Repositories.Generic;
using static BlogCenter.WebAPI.Dtos.RequestDto.GetBlogDto;

namespace BlogCenter.WebAPI.Repositories.Blog
{
    public interface IBlogRepository : IBaseRepository<Models.Models.Blog>
    {
        Task<Models.Models.Blog> AddBlogAsync(Models.Models.Blog blog, long userId);
        Task<ApiResponse> DeleteBlogById(long id, long? userId);
        Task<Models.Models.Blog> GetBlogById(long id);
        Task<List<Models.Models.Blog>> GetBlogsByUserId(string? searchString, string? sortString, long userId);
        Task<ApiPaginationResponse<GetBlog>> GetBlogsWithPaginationFilteringAndSortingAsync(string searchString, string searchTable, string sortString, int page, int pageSize, long userId);
        void UpdateBlog(Models.Models.Blog blog);
    }
}
