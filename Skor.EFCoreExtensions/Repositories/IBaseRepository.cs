using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Skor.EFCoreExtensions.Repositories
{
    public interface IBaseRepository<TEntity>
    {
        /// <summary>
        /// Get TEntity with primary key Id
        /// </summary>
        /// <param name="id">Primary Key</param>
        /// <returns>TEntity or new TEntity() (without tracking)</returns>
        Task<TEntity> GetAsync(object id);
        /// <summary>
        /// Get TEntity with primary keys Id[]
        /// </summary>
        /// <param name="id">Primary keys</param>
        /// <returns>TEntity or new TEntity() (without tracking)</returns>
        Task<TEntity> GetAsync(params object[] id);
        /// <summary>
        /// Get TEntity with predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>TEntity or new TEntity() (withou tracking)</returns>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Get IEnumerable<TEntity> without tracking
        /// </summary>
        /// <param name="page">Number of page want to take</param>
        /// <param name="take">TEntities per page</param>
        /// <returns>IEnumerable of TEntity (without tracking)</returns>
        Task<IEnumerable<TEntity>> GetAllAsync(int page=0, int take=0);
        /// <summary>
        /// Get IEnumerable<TEntity> with predicate without tracking
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="page">Number of page want to take</param>
        /// <param name="take">TEntities per page</param>
        /// <returns>IEnumerable of TEntity (without tracking)</returns>
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate,int page=0, int take=0);

        /// <summary>
        /// Add TEntity and return TEntity without tracking
        /// </summary>
        /// <param name="entity">TEntity</param>
        /// <returns>TEntity or new TEntity() (without tracking)</returns>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// Add IEnumerable<TEntity> and return IEnumerable<TEntity> without tracking
        /// </summary>
        /// <param name="entities">IEnumerable of TEntity</param>
        /// <returns>IEnumerable of TEntity or Empty IEnumerable of TEntity (without tracking)</returns>
        Task<IEnumerable<TEntity>> AddAsync(IEnumerable<TEntity> entities);
        /// <summary>
        /// Update TEntity (TEntity must no tracking)
        /// </summary>
        /// <param name="entity">TEntity</param>
        /// <returns>TEntity or new TEntity() (without tracking)</returns>
        Task<TEntity> UpdateAsync(TEntity entity);
        /// <summary>
        /// Update IEnumerable of TEntity
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>IEnumerable of TEntity or Empty IEnumerable of TEntity (without tracking)</returns>
        Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Delete TEntity (TEntity must no tracking)
        /// </summary>
        /// <param name="entity">TEntity</param>
        /// <returns>True if deleted or False if failed</returns>
        Task<bool> DeleteAsync(TEntity entity);

        /// <summary>
        /// Delete IEnumerable<TEntity> with predicate
        /// </summary>
        /// <param name="predicate"> Expression<Func<TEntity, bool>> </param>
        /// <returns>True if deleted or False if failed</returns>
        Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// Get TEntity with primary key Id
        /// </summary>
        /// <param name="id">Primary Key</param>
        /// <returns>TEntity or new TEntity() (without tracking)</returns>
        TEntity Get(object id);
        /// <summary>
        /// Get TEntity with primary keys Id[]
        /// </summary>
        /// <param name="id">Primary Keys</param>
        /// <returns>TEntity or new TEntity() (without tracking)</returns>
        TEntity Get(params object[] id);
        /// <summary>
        /// Get TEntity with predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>TEntity or new TEntity() (withou tracking)</returns>
        TEntity Get(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// Get IEnumerable<TEntity> without tracking
        /// </summary>
        /// <param name="page">Number of page want to take</param>
        /// <param name="take">TEntities per page</param>
        /// <returns>IEnumerable of TEntity (without tracking)</returns>
        IEnumerable<TEntity> GetAll(int page=0, int take=0);
        /// <summary>
        /// Get IEnumerable<TEntity> with predicate without tracking
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="page">Number of page want to take</param>
        /// <param name="take">TEntities per page</param>
        /// <returns>IEnumerable of TEntity (without tracking)</returns>
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate,int page=0, int take=0);
        /// <summary>
        /// Add TEntity and return TEntity without tracking
        /// </summary>
        /// <param name="entity">TEntity</param>
        /// <returns>TEntity or new TEntity() (without tracking)</returns>
        TEntity Add(TEntity entity);
        /// <summary>
        /// Add IEnumerable<TEntity> and return IEnumerable<TEntity> without tracking
        /// </summary>
        /// <param name="entities">IEnumerable of TEntity</param>
        /// <returns>IEnumerable of TEntity or Empty IEnumerable of TEntity (without tracking)</returns>
        IEnumerable<TEntity> Add(IEnumerable<TEntity> entity);
        /// <summary>
        /// Update TEntity (TEntity must no tracking)
        /// </summary>
        /// <param name="entity">TEntity</param>
        /// <returns>TEntity or new TEntity() (without tracking)</returns>
        TEntity Update(TEntity entity);
        /// <summary>
        /// Update IEnumerable of TEntity
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>IEnumerable of TEntity or Empty IEnumerable of TEntity (without tracking)</returns>
        IEnumerable<TEntity> Update(IEnumerable<TEntity> entities);
        /// <summary>
        /// Delete TEntity (TEntity must no tracking)
        /// </summary>
        /// <param name="entity">TEntity</param>
        /// <returns>True if deleted or False if failed</returns>
        bool Delete(TEntity entity);
        /// <summary>
        /// Delete IEnumerable<TEntity> with predicate
        /// </summary>
        /// <param name="predicate"> Expression<Func<TEntity, bool>> </param>
        /// <returns>True if deleted or False if failed</returns>
        bool Delete(Expression<Func<TEntity, bool>> predicate);
    }
}
