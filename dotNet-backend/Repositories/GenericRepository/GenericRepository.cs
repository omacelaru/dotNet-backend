using dotNet_backend.Models.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace dotNet_backend.Repositories.GenericRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly ApplicationDbContext _applicationDbContext;
        protected readonly DbSet<TEntity> _table;

        public GenericRepository(ApplicationDbContext lab4Context)
        {
            _applicationDbContext = lab4Context;
            _table = _applicationDbContext.Set<TEntity>();
        }
        
        // Create
        public async Task CreateAsync(TEntity entity)
        {
            await _table.AddAsync(entity);
        }
        public void Update(TEntity entity)
        {
            _table.Update(entity);
        }
        // Delete
        public void Delete(TEntity entity)
        {
            _table.Remove(entity);
        }
        public async Task<TEntity> FindSingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _table.SingleOrDefaultAsync(predicate);
        }
        public async Task<bool> SaveAsync()
        {
            return await _applicationDbContext.SaveChangesAsync() > 0;
        }
    }
}
