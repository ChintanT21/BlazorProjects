using BlogCenter.WebAPI.Dtos;
using BlogCenter.WebAPI.Dtos.Mapper;
using BlogCenter.WebAPI.Dtos.ResponceDto;
using BlogCenter.WebAPI.Models.Models;
using BlogCenter.WebAPI.Repositories.Generic;
using BlogCenter.WebAPI.Repositories.Utils;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Net;
using static BlogCenter.WebAPI.Dtos.Enums.Enums;
using static BlogCenter.WebAPI.Dtos.RequestDto.GetBlogDto;

namespace BlogCenter.WebAPI.Repositories.Blog
{
    public class BlogRepository(ApplicationDbContext _dbContext) : BaseRepository<Models.Models.Blog>(_dbContext), IBlogRepository
    {
        ApiResponse apiResponse = new();

        public async Task<Models.Models.Blog> AddBlogAsync(Models.Models.Blog addblog, long userId)
        {
            if (addblog != null)
            {
                Models.Models.Blog blog = await AddAsync(addblog);
                blog.CreatedBy = userId;
                blog.Status = 2;

                _dbContext.SaveChanges();
                return addblog;
            }
            return new Models.Models.Blog();

        }

        public async Task<ApiResponse> DeleteBlogById(long id, long? userId)
        {
            Models.Models.Blog blog = await GetByIdAsync(id);
            if (blog != null)
            {
                //_baseRepository.DeleteAsync(blog);
                blog.Status = 5;
                blog.UpdatedBy = userId;
                blog.UpdatedDate = DateTime.Now;
                _dbContext.SaveChanges();
                List<BlogsCategory> blogsCategory = GetBlogsCategoriesByBlogId(id);
                if (blogsCategory != null)
                {
                    foreach (var blogCategory in blogsCategory)
                    {
                        blogCategory.IsDeleted = true;
                    }
                }
                return apiResponse = new()
                {
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                    Result = blog
                };
            }
            return apiResponse = new()
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.NoContent,
            };

        }
        
        public async Task<Models.Models.Blog> GetBlogById(long id = 1)
        {
            Expression<Func<Models.Models.Blog, bool>> where = b => true;
            where = where.And(b => b.Id == id);
            Expression<Func<Models.Models.Blog, object>> includeCategory = b => b.BlogsCategories;
            Expression<Func<Models.Models.Blog, object>> includeCreatedByUser = b => b.CreatedByNavigation;
            Expression<Func<Models.Models.Blog, object>> includeUpdatedByUser = b => b.UpdatedByNavigation;
            Models.Models.Blog blog = await GetOneAsync(where,null, new[] { includeCategory, includeCreatedByUser, includeUpdatedByUser });
            return blog;
        }
        
        public async Task<ApiPaginationResponse<GetBlog>> GetBlogsWithPaginationFilteringAndSortingAsync(string searchString, string searchTable, string sortString, int page, int pageSize, long userId)
        {
            Expression<Func<Models.Models.Blog, bool>> where = b => true;
            if (userId != 0)
            {
                where = where.And(b => b.CreatedBy == userId);
            }

                Expression<Func<Models.Models.Blog, object>> includeCategory = b => b.BlogsCategories;
                Expression<Func<Models.Models.Blog, object>> includeCreatedByUser = b => b.CreatedByNavigation;
                Expression<Func<Models.Models.Blog, object>> includeUpdatedByUser = b => b.UpdatedByNavigation;
            // Apply search string filter to the where expression
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                if (!string.IsNullOrWhiteSpace(searchTable))
                {
                    switch (searchTable.ToLower())
                    {
                        case "title":
                            where = where.And(b => b.Title.ToLower().Contains(searchString));
                            break;
                        case "content":
                            where = where.And(b => b.Content.ToLower().Contains(searchString));
                            break;
                        case "status":
                            if (short.TryParse(searchString, out short statusValue) && statusValue!= 0)
                            {
                                where = where.And(b => b.Status==(statusValue));
                            }
                            break;
                        case "user":
                            if (long.TryParse(searchString, out long statusUserId) && statusUserId != 0)
                            {
                                where = where.And(b => b.CreatedBy == (statusUserId));
                            }
                            break;
                        default:
                            searchString = searchString.Trim().ToLower();
                            where = where.And(b => b.Title.ToLower().Contains(searchString) || b.Content.ToLower().Contains(searchString));
                            break;
                    }
                }
               
            }

            // Apply sorting
            Func<IQueryable<Models.Models.Blog>, IOrderedQueryable<Models.Models.Blog>>? orderBy = null;

            sortString = "CreatedDate,UpdatedDate";
            if (!string.IsNullOrWhiteSpace(sortString))
            {
                var sortFields = sortString.Split(',');
                orderBy = query =>
                {
                    IOrderedQueryable<Models.Models.Blog> orderedQuery = null!;
                    foreach (var field in sortFields)
                    {
                        string sortOrder = "ascending";
                        var sortField = field.Trim();
                        if (sortField.StartsWith("-"))
                        {
                            sortField = sortField.TrimStart('-');
                            sortOrder = "descending";
                        }

                        var parameter = Expression.Parameter(typeof(Models.Models.Blog), "b");
                        var property = Expression.Property(parameter, sortField);
                        var lambda = Expression.Lambda(property, parameter);

                        if (orderedQuery == null)
                        {
                            orderedQuery = sortOrder == "ascending"
                                ? Queryable.OrderBy(query, (dynamic)lambda)
                                : Queryable.OrderByDescending(query, (dynamic)lambda);
                        }
                        else
                        {
                            orderedQuery = sortOrder == "ascending"
                                ? Queryable.ThenBy(orderedQuery, (dynamic)lambda)
                                : Queryable.ThenByDescending(orderedQuery, (dynamic)lambda);
                        }
                    }
                    return orderedQuery ?? query.OrderBy(x => 0);
                };
            }


            PagedItemResult<Models.Models.Blog> pagedBlogData = await GetAllWithPaginationAsync(page, pageSize, where, new[] { includeCategory, includeCreatedByUser, includeUpdatedByUser }, orderBy);
            ApiPaginationResponse<GetBlog> apiPaginationResponse = new()
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Result = pagedBlogData.Items.ToGetBlogList(),
                TotalPages = pagedBlogData.TotalPages,
                TotalCount = pagedBlogData.TotalCount,
            };
            return apiPaginationResponse;
        }
       
        public List<BlogsCategory> GetBlogsCategoriesByBlogId(long id)
        {
            List<BlogsCategory> blogsCategory = [.. _dbContext.BlogsCategories.Where(x => x.BlogId == id)];

            return blogsCategory;
        }

        public async Task<ApiResponse> AddBlogCategoryById(BlogDto blogDto, long? userId)
        {

            if (blogDto != null)
            {
                Models.Models.Blog blog = await AddAsync(blogDto.ToBlog());

                blog.CreatedBy = userId ?? 1;
                _dbContext.SaveChanges();
                return apiResponse = new()
                {
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                    Result = blog
                };
            }
            return apiResponse = new()
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.NotFound,
                ErrorMessages = ["Server Error"]
            };
        }

        public async Task UpdateBlog(Models.Models.Blog blog)
        {
            await UpdateAsync(blog);
        }

        public async Task<List<Models.Models.Blog>> GetBlogsByUserId(string? searchString, string? sortString, long userId)
        {
            Expression<Func<Models.Models.Blog, bool>> byUserId = b => b.CreatedBy == userId;
            Expression<Func<Models.Models.Blog, object>> includeCategory = b => b.BlogsCategories;
            List<Models.Models.Blog> blogs = await GetAllAsync(byUserId, null, includeCategory);
            return blogs;
        }
    }
}
