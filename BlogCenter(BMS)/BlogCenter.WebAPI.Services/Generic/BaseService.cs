using BlogCenter.WebAPI.Dtos;
using BlogCenter.WebAPI.Models.Models;
using BlogCenter.WebAPI.Repositories.Generic;
using System.Linq.Expressions;

namespace BlogCenter.WebAPI.Repositories.Base
{
    public class BaseService<T> :IBaseService<T> where T : class 
    {
        private readonly IBaseRepository<T> _repository;
            
        public BaseService(IBaseRepository<T> repository)
        {
            _repository = repository;
        }
        public Task<T> AddAsync(T entity)
        {
            return _repository.AddAsync(entity);
        }

        public Task AddRangeAsync(List<T> entities)
        {
            return _repository.AddRangeAsync(entities);
        }

        public Task DeleteAsync(T entity)
        {
            return _repository.DeleteAsync(entity);
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? where = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params Expression<Func<T, object>>[] including)
        {
            List<T> list = await _repository.GetAllAsync(where, orderBy, including);
            return list;
        }
        public Task<PagedItemResult<T>> GetAllWithPaginationAsync(int page, int pageSize, Expression<Func<T, bool>>? whereCondition = null, Expression<Func<T, object>>[]? including = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        {
            return _repository.GetAllWithPaginationAsync(page, pageSize, whereCondition, including, orderBy);
        }

        public Task<List<T>> GetListAsync(Expression<Func<T, bool>>? where = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params Expression<Func<T, object>>[] including)
        {
            return _repository.GetListAsync(where, orderBy, including);
        }

        public Task<T> GetByIdAsync(long id, Expression<Func<T, bool>>? where = null, params Expression<Func<T, object>>[] including)
        {
            return _repository.GetByIdAsync(id, where, including);
        }

        public Task<T> GetOneAsync(Expression<Func<T, bool>>? where = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params Expression<Func<T, object>>[] including)
        {
            return _repository.GetOneAsync(where, orderBy, including);
        }

        public Task<T> UpdateAsync(T entity)
        {
            return _repository.UpdateAsync(entity);
        }
    }
}
