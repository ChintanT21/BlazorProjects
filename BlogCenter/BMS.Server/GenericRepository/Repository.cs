using BMS.Server.Models;
using BMS.Server.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BMS.Server.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbSet<T> _dbset;
        protected readonly ApplicationDbContext dbContext;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbset = dbContext.Set<T>();
            this.dbContext = dbContext;
        }

        public async Task<ApiResponse> AddAsync(T entity)
        {
            ApiResponse apiResponse = new();
            try
            {
                if (entity == null)
                {
                    return apiResponse = new()
                    {
                        IsSuccess = false,
                        StatusCode = HttpStatusCode.BadRequest,
                        ErrorMessages = ["no data found to add"]
                    };
                }
                await _dbset.AddAsync(entity);
                await dbContext.SaveChangesAsync();
                return apiResponse = new()
                {
                    IsSuccess = true,
                    StatusCode = (System.Net.HttpStatusCode)200,
                    Result=entity,
                };
            }
            catch (Exception ex)
            {
                return apiResponse = new()
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.NoContent,
                    ErrorMessages = new List<string>() { ex.ToString() }
                };
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
        public async Task<ApiResponse> DeleteAsync(T entity)
        {
            ApiResponse apiResponse = new();
            try
            {
                _dbset.Remove(entity);
                await dbContext.SaveChangesAsync();
                return apiResponse = new()
                {
                    StatusCode = HttpStatusCode.OK,
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                return apiResponse = new()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    IsSuccess = false,
                    ErrorMessages = new List<string>() { ex.ToString() }
                };
            }
        }
        public async Task<ApiResponse> UpdateAsync(T entity)
        {
            ApiResponse apiResponse = new();
            try
            {
                _dbset.Attach(entity);
                dbContext.Entry(entity).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();
                return apiResponse = new()
                {
                    StatusCode = HttpStatusCode.OK,
                    IsSuccess = true,
                };

            }
            catch (Exception ex)
            {
                return apiResponse = new()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    IsSuccess = false,
                    ErrorMessages = new List<string>() { ex.ToString() }
                };
            }
        }

        public async Task<ApiResponse> GetByIdAsync(int id)
        {
            ApiResponse apiResponse = new();

            try
            {
                var student = await _dbset.FindAsync(id);
                if (student == null)
                {
                    return apiResponse = new()
                    {
                        IsSuccess = false,
                        StatusCode = HttpStatusCode.NoContent,
                        ErrorMessages = new List<string>() { "not found at id : " + id }
                    };
                }
                return apiResponse = new()
                {
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                    Result = student,
                };
            }
            catch (Exception ex)
            {
                return apiResponse = new()
                {
                    IsSuccess = false,
                    StatusCode = (System.Net.HttpStatusCode)500,
                    ErrorMessages = new List<string>() { ex.ToString() }
                };
            }
        }
    }
}

