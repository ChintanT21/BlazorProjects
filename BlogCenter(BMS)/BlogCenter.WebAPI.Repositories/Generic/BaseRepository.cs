using BlogCenter.WebAPI.Models.Models;
using BMS.Client.Dtos;
using BMS.Server.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net;

namespace BlogCenter.WebAPI.Repositories.Generic
{
    public class BaseRepository<T>(ApplicationDbContext dbContext) : IBaseRepository<T> where T : class
    {
        protected DbSet<T> _dbset = dbContext.Set<T>();
        protected readonly ApplicationDbContext dbContext = dbContext;

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                if (entity != null)
                {
                    await _dbset.AddAsync(entity);
                    await dbContext.SaveChangesAsync();
                    return entity;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<ApiResponse> GetAllAsync()
        {
            ApiResponse apiResponse = new();
            try
            {
                return apiResponse = new()
                {
                    StatusCode = HttpStatusCode.OK,
                    IsSuccess = true,
                    Result = await _dbset.ToListAsync(),
                };

            }
            catch (Exception ex)
            {
                return apiResponse = new()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    IsSuccess = true,
                    ErrorMessages = new List<string>() { ex.ToString() }
                };
            }
        }
        public async Task<PagedItemResult<T>> GetAllWithPaginationAsync(int page, int pageSize, Expression<Func<T, bool>>? whereCondition = null)
        {
            IQueryable<T> items = _dbset;
            // Apply the where condition if it exists
            if (whereCondition != null)
            {
                items = items.Where(whereCondition);
            }
            // Apply pagination
            var totalCount = await _dbset.CountAsync();
            var totalItems = (int)Math.Ceiling(totalCount / (double)pageSize);
            var pagedItems = await items.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            var pagedBookData = new PagedItemResult<T>
            {
                Items = pagedItems,
                TotalCount = totalCount,
                TotalPages = totalItems
            };

            return pagedBookData;


        }
        public async void DeleteAsync(T entity)
        {
            try
            {
                if (entity != null)
                {
                    _dbset.Remove(entity);
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
            }
        }
        public async Task<T> UpdateAsync(T entity)
        {
            ApiResponse apiResponse = new();
            try
            {
                if (entity != null)
                {
                    _dbset.Attach(entity);
                    dbContext.Entry(entity).State = EntityState.Modified;
                    await dbContext.SaveChangesAsync();
                    return entity;
                }
                return null;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<T> GetByIdAsync(long id)
        {
            ApiResponse apiResponse = new();

            try
            {
                var Blog = await _dbset.FindAsync(id);
                if (Blog != null)
                {
                    return Blog;
                }
                return null;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
