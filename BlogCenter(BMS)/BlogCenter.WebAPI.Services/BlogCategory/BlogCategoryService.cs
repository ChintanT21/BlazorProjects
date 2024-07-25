using BlogCenter.WebAPI.Models.Models;
using BlogCenter.WebAPI.Repositories.Base;
using BlogCenter.WebAPI.Repositories.BlogCategory;

namespace BlogCenter.WebAPI.Services.BlogCategory
{
    public class BlogCategoryService(IBlogCategoryRepository _blogCategoryRepository,IBaseService<BlogsCategory> _baseService) : IBlogCategoryService
    {
         public Task<List<BlogsCategory>> GetByBlogId(long id)
        {
           return _blogCategoryRepository.GetByBlogId(id);
        }

         public Task<List<BlogsCategory>> GetAllCategory()
        {
            return _baseService.GetAllAsync();
        }
    }
}
