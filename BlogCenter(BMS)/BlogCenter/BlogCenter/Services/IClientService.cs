using BlogCenter.WebAPI.Dtos;
using BlogCenter.WebAPI.Dtos.RequestDto;
using BlogCenter.WebAPI.Dtos.ResponceDto;
using static BlogCenter.WebAPI.Dtos.RequestDto.GetBlogDto;

namespace BlogCenter.Blazor.Services
{
    public interface IClientService
    {
        Task<BlogTableDto> GetBlogData(DataManipulationDto dto);
        Task<bool> UpdateBlog(BlogDto blog);
        Task<GetBlog> GetOneBlog(long blogId);
        Task<object> ValidateCredential(LoginDto loginDto);
        Task<bool> CreateBlog(AddBlogDto blog);
        Task<List<GetCategoryDto>?> GetAllCategories();
        Task<bool> ChangeStatus(long blogId, int? statusId);
    }
}
