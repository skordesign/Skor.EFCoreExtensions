using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Skor.EFCoreExtensions.Repositories
{
    public interface IBaseRepository<TEntity>
    {
        Task<TEntity> GetAsync(long id);
        Task<TEntity> GetAsync(params long[] id);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetAllAsync(int page=0, int take=0);
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate,int page=0, int take=0);
        Task<TEntity> AddAsync(TEntity entity);
        Task<IEnumerable<TEntity>> AddAsync(IEnumerable<TEntity> entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities);
        Task<bool> DeleteAsync(TEntity entity);
        Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> predicate);
        TEntity Get(long id);
        TEntity Get(params long[] id);
        TEntity Get(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetAll(int page=0, int take=0);
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate,int page=0, int take=0);
        TEntity Add(TEntity entity);
        IEnumerable<TEntity> Add(IEnumerable<TEntity> entity);
        TEntity Update(TEntity entity);
        IEnumerable<TEntity> Update(IEnumerable<TEntity> entities);
        bool Delete(TEntity entity);
        bool Delete(Expression<Func<TEntity, bool>> predicate);
    }
}
