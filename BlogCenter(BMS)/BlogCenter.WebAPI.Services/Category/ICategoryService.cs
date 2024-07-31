using BlogCenter.WebAPI.Dtos;
using BlogCenter.WebAPI.Dtos.RequestDto;
using BlogCenter.WebAPI.Dtos.ResponceDto;
using BlogCenter.WebAPI.Models.Models;
using BlogCenter.WebAPI.Repositories.Base;
using BlogCenter.WebAPI.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCenter.WebAPI.Services.Category
{
    public interface ICategoryService : IBaseService<Models.Models.Category>
    {
        Task<GetCategoryDto> AddCategory(long createdBy, CategoryDto.AddCategoryDto addCategoryDto);
        Task<bool> DeleteCategory(long updatedBy, int categoryId);
        Task<List<Models.Models.Category>> GetAll();
        Task<List<long>> GetBlogsByCategoryId(int categoryId);
        Task<ApiPaginationResponse<GetCategoryDto>> GetCategoriesPageWise(string searchString, string searchTable, string sortString, int page, int pageSize, long userId);
        Task<GetCategoryDto> GetCategoryById(int categoryId);
        Task<bool> IsCategoryExits(string name);
        Task<GetCategoryDto> UpdateCategory(long updatedBy, CategoryDto.UpdateCategoryDto updateCategoryDto);
    }
}
