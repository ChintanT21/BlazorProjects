using BlogCenter.WebAPI.Dtos.ResponceDto;
using BlogCenter.WebAPI.Models.Models;
using BMS.Server.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCenter.WebAPI.Repositories.BlogCategory
{
    public interface IBlogCategoryRepository
    {
        Task<List<BlogsCategory>> GetByBlogId(long id);
        Task<ApiResponse> AddAsync(BlogDto blogDto, long? userId);
        Task<ApiResponse> DeleteBlogId(long id, long? userId);
        object AddBlogCategoryAsync(long blogId,List<int> categories, long? userId);
        void UpdateBlogCategory(int blogId, List<int> categories, long? id);
    }
}
