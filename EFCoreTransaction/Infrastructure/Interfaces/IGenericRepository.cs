using System;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace EFCoreTransaction.Infrastructure.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        public Task<IEnumerable<TEntity>> GetEntityCollectionByFilterAsync(Expression<Func<TEntity, bool>> expression);

        public Task<TEntity> GetEntityByFilterAsync(Expression<Func<TEntity, bool>> expression);

        public Task<TEntity> GetEntityByIdAsync(int id);

        public Task AddAsync(TEntity entity);

        public Task UpdateAsync(TEntity entity);

        public Task RemoveAsync(TEntity entity);
    }
}
