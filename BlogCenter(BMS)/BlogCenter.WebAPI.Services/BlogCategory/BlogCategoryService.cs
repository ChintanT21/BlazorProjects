using BlogCenter.WebAPI.Models.Models;
using BlogCenter.WebAPI.Repositories.BlogCategory;

namespace BlogCenter.WebAPI.Services.BlogCategory
{
    public class BlogCategoryService(IBlogCategoryRepository _blogCategoryRepository) : IBlogCategoryService
    {
        public Task<List<BlogsCategory>> GetByBlogId(long id)
        {
           return _blogCategoryRepository.GetByBlogId(id);
        }
    }
}
