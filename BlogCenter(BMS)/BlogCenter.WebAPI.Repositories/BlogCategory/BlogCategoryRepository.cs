using BlogCenter.WebAPI.Dtos;
using BlogCenter.WebAPI.Dtos.ResponceDto;
using BlogCenter.WebAPI.Models.Models;
using BlogCenter.WebAPI.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace BlogCenter.WebAPI.Repositories.BlogCategory
{
    public class BlogCategoryRepository(ApplicationDbContext _dbContext) : BaseRepository<Models.Models.BlogsCategory>(_dbContext), IBlogCategoryRepository
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
            base.AddRangeAsync(BlogCategories);
            return BlogCategories;
        }

        public Task<ApiResponse> DeleteBlogId(long id, long? userId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BlogsCategory>> GetAllCategory()
        {
            List<BlogsCategory> blogsCategory=await GetAllAsync();
            return blogsCategory;
        }

        public async Task<ICollection<BlogsCategory>> GetByBlogId(long blogId)
        {
            List<BlogsCategory> blogsCategory = await _dbContext.BlogsCategories.Where(b => b.IsDeleted == false && b.BlogId == blogId).ToListAsync();

            return blogsCategory;
        }

        public async Task UpdateBlogCategory(int blogId, List<int> categories, long? id)
        {
            if (id != null)
            {
                ICollection<BlogsCategory> IblogsCategories = _dbContext.BlogsCategories.Where(b => b.IsDeleted == false && b.BlogId == blogId).ToList();
                List<BlogsCategory> blogCategories = IblogsCategories.ToList();

                foreach (BlogsCategory category in blogCategories)
                {
                    if (!categories.Contains(category.CategoryId))
                    {
                        category.IsDeleted = true;
                        await UpdateAsync(category);
                    }
                }


                List<BlogsCategory> newCategories = new();
                foreach (var categoryId in categories)
                {

                    BlogsCategory? existingCategory = blogCategories.FirstOrDefault(ec => ec.CategoryId == categoryId);
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
                        await AddAsync(newCategory);
                    }
                    else
                    {

                        if (existingCategory.IsDeleted)
                        {
                            existingCategory.IsDeleted = false;
                            await UpdateAsync(existingCategory);
                        }
                    }
                }
            }

        }
    }
}

