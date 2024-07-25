using BlogCenter.WebAPI.Dtos;
using BlogCenter.WebAPI.Dtos.RequestDto;
using BlogCenter.WebAPI.Dtos.ResponceDto;

namespace BlogCenter.Blazor.Services
{
    public interface IClientService
    {
        Task<BlogTableDto> GetBlogData(DataManipulationDto dto);
        Task<object> ValidateCredential(LoginDto loginDto);
        Task<bool> CreateBlog(AddBlogDto blog);
        Task<List<GetCategoryDto>?> GetAllCategories();

    }
}
