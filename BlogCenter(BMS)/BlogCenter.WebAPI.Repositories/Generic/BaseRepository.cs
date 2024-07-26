using BlogCenter.WebAPI.Dtos;
using BlogCenter.WebAPI.Models.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlogCenter.WebAPI.Repositories.Generic
{
    public class BaseRepository<T>(ApplicationDbContext dbContext) : IBaseRepository<T> where T : class
    {
        protected DbSet<T> _dbset = dbContext.Set<T>();
        protected readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<T> AddAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                await _dbset.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here for brevity)
                throw;
            }
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? where = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params Expression<Func<T, object>>[] including)
        {
            try
            {
                IQueryable<T> query = _dbset;

                if (where != null)
                {
                    query = query.Where(where);
                }

                if (including != null && including.Length != 0)
                {
                    foreach (var include in including)
                    {
                        query = query.Include(include);
                    }
                }
                

                if (orderBy != null)
                {
                    query = orderBy(query);
                }

                return await query.ToListAsync();

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<PagedItemResult<T>> GetAllWithPaginationAsync(int page,
            int pageSize,
            Expression<Func<T, bool>>? whereCondition,
            Expression<Func<T, object>>[]? including,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy)
        {
            IQueryable<T> items = _dbset;
            if (whereCondition != null)
            {
                items = items.Where(whereCondition);
            }

            if (including != null)
            {
                foreach (var include in including)
                {
                    items = items.Include(include);
                }
            }
            if (orderBy != null)
            {
                items = orderBy(items);
            }
            // Apply pagination
            var totalCount = await items.CountAsync();
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
        public async Task DeleteAsync(T entity)
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
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                _dbset.Attach(entity);
                dbContext.Entry(entity).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();
                return entity;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine($"Concurrency exception: {ex.Message}");
                throw;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database update exception: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<T> GetByIdAsync(long id, Expression<Func<T, bool>>? where = null, params Expression<Func<T, object>>[] including)
        {
            try
            {
                IQueryable<T> query = _dbset;
                if (where != null)
                {
                    query = query.Where(where);
                }

                foreach (var include in including)
                {
                    query = query.Include(include);
                }
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
        public async Task AddRangeAsync(List<T> entities)
        {
            try
            {
                if (entities != null)
                {
                    await _dbset.AddRangeAsync(entities);
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
            }

        }

        public async Task<IQueryable<T>> GetByIdAllAsync(long id, Expression<Func<T, bool>>? where = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params Expression<Func<T, object>>[] including)
        {
            IQueryable<T> items = _dbset;
            if (where != null)
            {
                items = items.Where(where);
            }

            if (including != null)
            {
                foreach (var include in including)
                {
                    items = items.Include(include);
                }
            }
            if (orderBy != null)
            {
                items = orderBy(items);
            }
            var itemsList = await items.ToListAsync();
            return itemsList.AsQueryable();
        }
    }
}
