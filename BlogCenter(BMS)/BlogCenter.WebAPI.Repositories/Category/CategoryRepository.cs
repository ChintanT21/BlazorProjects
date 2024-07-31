using BlogCenter.WebAPI.Dtos.RequestDto;
using BlogCenter.WebAPI.Models.Models;
using BlogCenter.WebAPI.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCenter.WebAPI.Repositories.Category
{
    public class CategoryRepository(ApplicationDbContext _dbContext) : BaseRepository<Models.Models.Category>(_dbContext),ICategoryRepository
    {
        public async Task<List<long>> GetBlogsByCategoryId(int categoryId)
        {

            List<long> blogIdList = _dbContext.BlogsCategories
                                    .Where(bc => bc.CategoryId == categoryId)
                                    .Select(bc => bc.BlogId)
                                    .ToList();
            return blogIdList;
        }
    }
}
