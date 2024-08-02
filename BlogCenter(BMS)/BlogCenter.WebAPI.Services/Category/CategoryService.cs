using BlogCenter.WebAPI.Dtos;
using BlogCenter.WebAPI.Dtos.Mapper;
using BlogCenter.WebAPI.Dtos.RequestDto;
using BlogCenter.WebAPI.Dtos.ResponceDto;
using BlogCenter.WebAPI.Models.Models;
using BlogCenter.WebAPI.Repositories.Base;
using BlogCenter.WebAPI.Repositories.Category;
using BlogCenter.WebAPI.Repositories.Generic;
using BlogCenter.WebAPI.Repositories.Utils;
using System.Linq.Expressions;
using System.Net;
using System.Xml.Linq;
using static BlogCenter.WebAPI.Dtos.ResponceDto.CategoryDto;

namespace BlogCenter.WebAPI.Services.Category
{
    public class CategoryService(ICategoryRepository _categoryRepository, IBaseRepository<Models.Models.BlogsCategory> _baseRepository) : BaseService<Models.Models.Category>(_categoryRepository), ICategoryService
    {
        Models.Models.Category category = new();
        public async Task<GetCategoryDto> AddCategory(long createdBy, CategoryDto.AddCategoryDto addCategoryDto)
        {
            GetCategoryDto getCategoryDto = new();
            category = addCategoryDto.ToCategoryFromAddCategoryDto(createdBy);
            category = await AddAsync(category);
            getCategoryDto = category.MapToGetCategoryDto();

            return getCategoryDto;
        }

        public async Task<bool> DeleteCategory(long updatedBy, int categoryId)
        {
            Expression<Func<Models.Models.Category, bool>> where = b => true;
            where = where.And(b => b.Id == categoryId);
            category = await GetOneAsync(where, null, null);
            category.UpdatedBy = updatedBy;
            category.UpdatedDate= DateTime.Now;
            category.IsDeleted = true;
            category = await UpdateAsync(category);
            if (category != null)
            {
                return true;
            }
            return false;
        }

        public async Task<List<Models.Models.Category>> GetAll()
        {
            Expression<Func<Models.Models.Category, bool>> where = b => b.IsDeleted == false;
            List<Models.Models.Category> result = await GetAllAsync(where);
            return result;
        }

        public async Task<GetCategoryDto> UpdateCategory(long updatedBy, CategoryDto.UpdateCategoryDto updateCategoryDto)
        {
            Expression<Func<Models.Models.Category, bool>> where = b => b.Id == updateCategoryDto.Id;
            category = await GetOneAsync(where, null, null);
            category.IsDeleted = updateCategoryDto.IsDeleted != null ? (bool)updateCategoryDto.IsDeleted : category.IsDeleted;
            category.Name = updateCategoryDto.Name != null ? updateCategoryDto.Name : category.Name;
            category.UpdatedBy = updatedBy;
            category.UpdatedDate = DateTime.Now;
            category = await UpdateAsync(category);
            return category.MapToGetCategoryDto();
        }

        public async Task<ApiPaginationResponse<GetCategoryDto>> GetCategoriesPageWise(string searchString, string searchTable, string sortString, int page, int pageSize, long userId)
        {
            Expression<Func<Models.Models.Category, bool>> where = b => true;
            if (userId != 0)
            {
                where = where.And(b => b.CreatedBy == userId);
            }

            // Apply search string filter to the where expression
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                if (!string.IsNullOrWhiteSpace(searchTable))
                {
                    switch (searchTable.ToLower())
                    {
                        case "name":
                            searchString = searchString.Trim().ToLower();
                            where = where.And(b => b.Name.ToLower().Contains(searchString));
                            break;
                        case "status":
                            switch (searchString)
                            {
                                case "All":
                                    break;
                                case "Deleted":
                                    where = where.And(b => b.IsDeleted == true);
                                    break;
                                case "Active":
                                    where = where.And(b => b.IsDeleted == false);
                                    break;
                            }
                            break;
                        default:
                            // Handle unknown searchTable value or do nothing
                            break;
                    }
                }
            }

            // Apply sorting
            Func<IQueryable<Models.Models.Category>, IOrderedQueryable<Models.Models.Category>>? orderBy = null;

            sortString = "CreatedDate,UpdatedDate";
            if (!string.IsNullOrWhiteSpace(sortString))
            {
                var sortFields = sortString.Split(',');
                orderBy = query =>
                {
                    IOrderedQueryable<Models.Models.Category> orderedQuery = null!;
                    foreach (var field in sortFields)
                    {
                        string sortOrder = "ascending";
                        var sortField = field.Trim();
                        if (sortField.StartsWith("-"))
                        {
                            sortField = sortField.TrimStart('-');
                            sortOrder = "descending";
                        }

                        var parameter = Expression.Parameter(typeof(Models.Models.Category), "b");
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


            PagedItemResult<Models.Models.Category> pagedBlogData = await GetAllWithPaginationAsync(page, pageSize, where, null, orderBy);
            ApiPaginationResponse<GetCategoryDto> apiPaginationResponse = new()
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Result = pagedBlogData.Items.MapToGetCategoryDtoList(),
                TotalPages = pagedBlogData.TotalPages,
                TotalCount = pagedBlogData.TotalCount,
            };
            return apiPaginationResponse;
        }

        public async Task<GetCategoryDto> GetCategoryById(int categoryId)
        {
            Expression<Func<Models.Models.Category, bool>> where = b => b.Id == categoryId;
            category = await GetOneAsync(where, null, null);
            return category.MapToGetCategoryDto();
        }

        public async Task<bool> IsCategoryExits(string name)
        {
            Expression<Func<Models.Models.Category, bool>> where = b => b.Name.ToLower().Trim() == name.ToLower().Trim();
            category = await GetOneAsync(where, null, null);
            if (category != null && category.Name.ToLower().Trim() == name.ToLower().Trim())
            {
                return true;
            }
            return false;
        }

        public async Task<List<long>> GetBlogsByCategoryId(int categoryId)
        {
            Expression<Func<Models.Models.BlogsCategory, bool>> where = b => b.CategoryId==categoryId;
            List<Models.Models.BlogsCategory> categories = await _baseRepository.GetListAsync(where,null,null);
            List<long> blogIdList = categories.Select(b => b.BlogId).ToList();
            return blogIdList;
        }
    }
}
