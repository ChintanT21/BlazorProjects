using BlogCenter.WebAPI.Dtos.RequestDto;
using BlogCenter.WebAPI.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCenter.WebAPI.Repositories.Category
{
    public interface ICategoryRepository:IBaseRepository<Models.Models.Category>
    {
        Task<List<long>> GetBlogsByCategoryId(int categoryId);
    }
}
