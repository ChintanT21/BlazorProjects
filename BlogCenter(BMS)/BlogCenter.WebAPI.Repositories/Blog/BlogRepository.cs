using BlogCenter.WebAPI.Dtos.RequestDto;
using BlogCenter.WebAPI.Models.Models;
using BlogCenter.WebAPI.Repositories.Generic;
using BMS.Server.ViewModels;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using BMS.Client.Dtos;
using System.Net;
using BlogCenter.WebAPI.Dtos.ResponceDto;

namespace BlogCenter.WebAPI.Repositories.Blog
{
    public class BlogRepository(ApplicationDbContext _dbContext, IBaseRepository<Models.Models.Blog> _baseRepository) : IBlogRepository
    {
        ApiResponse apiResponse = new();

        public async Task<ApiResponse> AddBlogAsync(BlogDto blogDto)
        {
            Models.Models.Blog blog = await _baseRepository.AddAsync(blogDto);
        }

        public async Task<ApiResponse> DeleteBlogById(long id)
        {
            Models.Models.Blog blog = await _baseRepository.GetByIdAsync(id);
            if (blog != null)
            {
                _baseRepository.DeleteAsync(blog);
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
        public async Task<ApiResponse> GetBlogById(long id=1)
        {
            Models.Models.Blog blog = await _baseRepository.GetByIdAsync(id);
            if (blog != null)
            {
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
        public async Task<ApiPaginationResponse> GetBlogsWithPaginationFilteringAndSortingAsync(string? searchString, string? sortString, int page, int pageSize)
        {
            PagedItemResult<Models.Models.Blog> pagedBlogData = await _baseRepository.GetAllWithPaginationAsync(page, pageSize);
            List<Models.Models.Blog> blogs = pagedBlogData.Items.ToList();
            if (pagedBlogData is PagedItemResult<Models.Models.Blog> pagedBlogResult)
            {
                List<Models.Models.Blog> blogList = pagedBlogResult.Items;

                // (arrange order title ascending and year descending
                if (!string.IsNullOrWhiteSpace(searchString))
                {
                    if (string.IsNullOrWhiteSpace(searchString))
                        blogs = blogList;
                    else
                    {
                        searchString = searchString.Trim().ToLower();
                        // filtering records with author or title
                        blogs = _dbContext
                            .Blogs
                            .Where(b => b.Title.ToLower().Contains(searchString)
                            || b.Content.ToLower().Contains(searchString)
                            ).ToList();
                    }
                }
                if (!string.IsNullOrWhiteSpace(sortString))
                {
                    // sorting
                    // sort=title,-ye
                    var sortFields = sortString.Split(','); // ['title','-year']
                    StringBuilder orderQueryBuilder = new StringBuilder();
                    // using reflection to get properties of book
                    // propertyInfo= [Id,Title,Year,Author,Language] 
                    PropertyInfo[] propertyInfo = typeof(Models.Models.Blog).GetProperties();


                    foreach (var field in sortFields)
                    {
                        // iteration 1, field=title
                        // iteration 2, field=-year
                        string sortOrder = "ascending";
                        // iteration 1, sortField= title
                        // iteration 2, sortField=-year
                        var sortField = field.Trim();
                        if (sortField.StartsWith("-"))
                        {
                            sortField = sortField.TrimStart('-');
                            sortOrder = "descending";
                        }
                        // property = 'Title'
                        // property = 'Year'
                        var property = propertyInfo.FirstOrDefault(a => a.Name.Equals(sortField, StringComparison.OrdinalIgnoreCase));
                        if (property == null)
                            continue;
                        // orderQueryBuilder= "Title ascending,Year descending, "
                        // it have trailing , and whitespace
                        orderQueryBuilder.Append($"{property.Name.ToString()} {sortOrder}, ");
                    }
                    // remove trailing , and whitespace here
                    // orderQuery = ""Title ascending,Year descending"
                    string orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
                    if (!string.IsNullOrWhiteSpace(orderQuery))
                        // use System.Linq.Dynamic.Core namespace for this
                        blogs = blogs.AsQueryable().OrderBy(orderQuery).ToList();
                    else
                        blogs = blogs.AsQueryable().OrderBy(orderQuery).ToList();
                }
                ApiPaginationResponse apiPaginationResponse = new()
                {
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                    Result = blogs,
                    TotalPages = pagedBlogData.TotalPages,
                    TotalCount = pagedBlogData.TotalCount,
                };
                return apiPaginationResponse;
            }
            else
            {
                ApiPaginationResponse apiPaginationResponse = new()
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.NoContent,

                };
                return apiPaginationResponse;
                // Handle the error or the case when the cast fails
                throw new InvalidCastException("ApiResponse.Result is not of type PagedItemResult<Models.Models.Blog>");
            }

        }
    }
}
