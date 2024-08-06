using BlogCenter.WebAPI.Dtos.RequestDto;
using BlogCenter.WebAPI.Dtos.ResponceDto;
using BlogCenter.WebAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BlogCenter.WebAPI.Dtos.RequestDto.GetBlogDto;

namespace BlogCenter.Shared.ClientServices.Blogs
{
    public interface IBlogClientService
    {
        Task<List<GetUserDto>> GetAllUsers();
        Task<BlogTableDto> GetBlogData(DataManipulationDto dto);
        Task<bool> UpdateBlog(BlogDto blog);
        Task<GetBlog> GetOneBlog(long blogId);
        Task<object> ValidateCredential(LoginDto loginDto);
        Task<bool> CreateBlog(AddBlogDto blog);
        Task<List<GetCategoryDto>?> GetAllCategories();
        Task<bool> ChangeStatus(long blogId, int? statusId);
    }
}
