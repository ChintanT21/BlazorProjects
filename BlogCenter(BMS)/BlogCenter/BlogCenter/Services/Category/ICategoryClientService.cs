using BlogCenter.Components.Pages.Admin.Categories;
using BlogCenter.WebAPI.Dtos;
using BlogCenter.WebAPI.Dtos.RequestDto;
using BlogCenter.WebAPI.Dtos.ResponceDto;
using static BlogCenter.WebAPI.Dtos.ResponceDto.CategoryDto;

namespace BlogCenter.Services.Category
{
    public interface ICategoryClientService
    {
        Task<ApiPaginationResponse<GetCategoryDto>> GetCategoriesData(DataManipulationDto dto);
        Task<Dictionary<bool, string>> AddCategory(AddCategoryDto dto);
        Task<Dictionary<bool, string>> UpdateCategory(UpdateCategoryDto dto);
        Task<Dictionary<bool, string>> DeleteCategory(int categoryId);
        Task<GetCategoryDto> GetCategoryById(int categoryId);
    }
}
