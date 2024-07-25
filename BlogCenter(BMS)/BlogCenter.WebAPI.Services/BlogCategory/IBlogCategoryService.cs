using BlogCenter.WebAPI.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCenter.WebAPI.Services.BlogCategory
{
    public interface IBlogCategoryService
    {
        Task<List<BlogsCategory>> GetAllCategory();
        Task<List<BlogsCategory>> GetByBlogId(long id);
    }
}
