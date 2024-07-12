using BlogCenter.WebAPI.Dtos.RequestDto;
using BlogCenter.WebAPI.Dtos.ResponceDto;
using BMS.Server.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCenter.WebAPI.Repositories.Blog
{
    public interface IBlogRepository
    {
        Task<ApiResponse> AddBlogAsync(BlogDto blogDto);
        Task<ApiResponse> DeleteBlogById(long id);
        Task<ApiResponse> GetBlogById(long id);
        Task<ApiPaginationResponse> GetBlogsWithPaginationFilteringAndSortingAsync(string? searchString, string? sortString,int page, int pageSize);
    }
}
