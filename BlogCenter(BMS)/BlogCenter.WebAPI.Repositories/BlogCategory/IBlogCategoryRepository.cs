using BlogCenter.WebAPI.Dtos;
using BlogCenter.WebAPI.Dtos.ResponceDto;
using BlogCenter.WebAPI.Models.Models;
using BlogCenter.WebAPI.Repositories.Generic;

namespace BlogCenter.WebAPI.Repositories.BlogCategory
{
    public interface IBlogCategoryRepository : IBaseRepository<Models.Models.BlogsCategory>
    {
        Task<ICollection<BlogsCategory>> GetByBlogId(long id);
        Task<ApiResponse> AddAsync(BlogDto blogDto, long? userId);
        Task<ApiResponse> DeleteBlogId(long id, long? userId);
        object AddBlogCategoryAsync(long blogId, List<int> categories, long? userId);
        Task UpdateBlogCategory(int blogId, List<int> categories, long? id);
        Task<List<BlogsCategory>> GetAllCategory();
    }
}
