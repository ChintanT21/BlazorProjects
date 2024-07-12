using BMS.Client.Dtos;
using BMS.Server.ViewModels;
using System.Linq.Expressions;

namespace BlogCenter.WebAPI.Repositories.Generic
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        void DeleteAsync(T entity);
        Task<T> GetByIdAsync(long id);
        Task<ApiResponse> GetAllAsync();
        Task<PagedItemResult<T>> GetAllWithPaginationAsync(int page, int pageSize, Expression<Func<T, bool>>? whereCondition = null);
 
    }
}
