using BlogCenter.WebAPI.Models.Models;
using BlogCenter.WebAPI.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCenter.WebAPI.Services.Category
{
    public class CategoryService(IBaseService<Models.Models.Category> _baseService) : ICategoryService
    {
        public async Task<List<Models.Models.Category>> GetAll()
        {
            List<Models.Models.Category> result = await _baseService.GetAllAsync();
            return result;
        }

    }
}
