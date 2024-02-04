using dotNet_backend.Models.Base;
using System.Linq.Expressions;

namespace dotNet_backend.Repositories.GenericRepository
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        // Create
        Task CreateAsync(TEntity entity);
        // Update
        void Update(TEntity entity);
        // Delete 
        void Delete(TEntity entity);
        // Find
        Task<TEntity> FindSingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        // Save
        Task<bool> SaveAsync();
    }
}
