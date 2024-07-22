using BlogCenter.WebAPI.Dtos.RequestDto;
using BlogCenter.WebAPI.Models.Models;
using BMS.Client.Dtos;
using BMS.Server.ViewModels;
using static BlogCenter.WebAPI.Dtos.RequestDto.GetBlogDto;

namespace BlogCenter.Blazor.Services
{
    public interface IClientService
    {
        Task<List<GetBlog>> GetBlogData();
        Task<object> ValidateCredential(LoginDto loginDto);
    }
}
