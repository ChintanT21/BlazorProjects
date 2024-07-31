using BlogCenter.WebAPI.Dtos;
using System.Linq.Expressions;

namespace BlogCenter.WebAPI.Repositories.Generic
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(List<T> entities);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> GetByIdAsync(long id, Expression<Func<T, bool>>? where = null, params Expression<Func<T, object>>[] including);
        Task<List<T>> GetListAsync(Expression<Func<T, bool>>? where = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params Expression<Func<T, object>>[] including);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? where = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params Expression<Func<T, object>>[] including);
        Task<PagedItemResult<T>> GetAllWithPaginationAsync(
            int page,
            int pageSize,
            Expression<Func<T, bool>>? whereCondition = null,
            Expression<Func<T, object>>[]? including = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);
        Task<T> GetOneAsync(Expression<Func<T, bool>>? where = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params Expression<Func<T, object>>[] including);
    }
}
