using BlogCenter.WebAPI.Dtos.ResponceDto;
using BlogCenter.WebAPI.Models.Models;
using BlogCenter.WebAPI.Repositories.Generic;
using BMS.Server.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogCenter.WebAPI.Repositories.BlogCategory
{
    public class BlogCategoryRepository(ApplicationDbContext _dbContext, IBaseRepository<BlogsCategory> _baseRepository) : IBlogCategoryRepository
    {
        public Task<ApiResponse> AddAsync(BlogDto blogDto, long? userId)
        {
            return null;
        }

        public object AddBlogCategoryAsync(long blogId, List<int> categories, long? userId)
        {
            List<BlogsCategory> BlogCategories = new List<BlogsCategory>();
            foreach (var category in categories)
            {
                BlogsCategory blogsCategory = new()
                {
                    BlogId = blogId,
                    CategoryId = category,
                    CreatedDate = DateTime.Now,
                    CreatedBy = (long)userId,
                };
                BlogCategories.Add(blogsCategory);
            }
            _baseRepository.AddRangeAsync(BlogCategories);
            return BlogCategories;
        }

        public Task<ApiResponse> DeleteBlogId(long id, long? userId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BlogsCategory>> GetByBlogId(long blogId)
        {
            List<BlogsCategory> blogsCategory = await _dbContext.BlogsCategories.Where(b => b.IsDeleted == false && b.BlogId == blogId).ToListAsync();

            return blogsCategory;
        }

        public async void UpdateBlogCategory(int blogId, List<int> categories, long? id)
        {
            if (id != null)
            {
                List<BlogsCategory> blogCategories = await GetByBlogId(blogId);

                foreach (var category in blogCategories)
                {
                    if (!categories.Contains(category.CategoryId))
                    {
                        category.IsDeleted = true;
                        await _baseRepository.UpdateAsync(category);
                    }
                }


                List<BlogsCategory> newCategories = new();
                foreach (var categoryId in categories)
                {

                    var existingCategory = blogCategories.FirstOrDefault(ec => ec.CategoryId == categoryId);
                    if (existingCategory == null)
                    {
                        BlogsCategory newCategory = new()
                        {
                            BlogId = blogId,
                            CategoryId = categoryId,
                            CreatedDate = DateTime.Now,
                            CreatedBy = (long)id,
                        };
                        newCategories.Add(newCategory);
                        await _baseRepository.AddAsync(newCategory);
                    }
                    else
                    {

                        if (existingCategory.IsDeleted)
                        {
                            existingCategory.IsDeleted = false;
                            await _baseRepository.UpdateAsync(existingCategory);
                        }
                    }
                }
            }

        }
    }
}

