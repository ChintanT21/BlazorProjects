using BMS_backend.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BMS_backend.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<ApiResponse> AddAsync(T entity);
        Task<ApiResponse> UpdateAsync(T entity);
        Task<ApiResponse> DeleteAsync(T entity);
        Task<ApiResponse> GetByIdAsync(int id);
        Task<ApiResponse> GetAllAsync();
    }
}
