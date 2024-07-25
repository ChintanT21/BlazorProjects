using BlogCenter.WebAPI.Dtos;
using BlogCenter.WebAPI.Repositories.Generic;
using System.Linq.Expressions;

namespace BlogCenter.WebAPI.Repositories.Base
{
    public class BaseService<T>(IBaseRepository<T> _baseRepository) : IBaseService<T> where T : class
    {
        public Task<T> AddAsync(T entity)
        {
           return _baseRepository.AddAsync(entity);
        }

        public Task AddRangeAsync(List<T> entities)
        {
            return _baseRepository.AddRangeAsync(entities);
        }

        public Task DeleteAsync(T entity)
        {
            return  _baseRepository.DeleteAsync(entity);
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? where = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params Expression<Func<T, object>>[] including)
        {
            List<T> list = await _baseRepository.GetAllAsync(where, orderBy, including);
            return list;
        }

        public Task<PagedItemResult<T>> GetAllWithPaginationAsync(int page, int pageSize, Expression<Func<T, bool>>? whereCondition = null, Expression<Func<T, object>>[]? including = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        {
            return _baseRepository.GetAllWithPaginationAsync(page, pageSize, whereCondition, including, orderBy);    
        }

        public Task<IQueryable<T>> GetByIdAllAsync(long id, Expression<Func<T, bool>>? where = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params Expression<Func<T, object>>[] including)
        {
           return _baseRepository.GetByIdAllAsync(id, where, orderBy, including);
        }

        public Task<T> GetByIdAsync(long id, Expression<Func<T, bool>>? where = null, params Expression<Func<T, object>>[] including)
        {
           return _baseRepository.GetByIdAsync(id,where, including);
        }

        public Task<T> UpdateAsync(T entity)
        {
            return _baseRepository.UpdateAsync(entity);
        }
    }
}
