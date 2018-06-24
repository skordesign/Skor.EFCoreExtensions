using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Skor.EFCoreExtensions.Repositories
{
    public class BaseRepository<TEntity, TDbContext> : IBaseRepository<TEntity>
        where TDbContext : DbContext
        where TEntity : class, new()
    {
        private readonly DbContext context;
        protected DbSet<TEntity> dbSet => this.context.Set<TEntity>();
        public BaseRepository(TDbContext ctxt)
        {
            this.context = ctxt;
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            try
            {
                using (var transaction = await this.context.Database.BeginTransactionAsync())
                {
                    await this.dbSet.AddAsync(entity);
                    await this.context.SaveChangesAsync();
                    transaction.Commit();
                    this.context.Entry(entity).State = EntityState.Detached;
                    return entity;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public async Task<IEnumerable<TEntity>> AddAsync(IEnumerable<TEntity> entities)
        {
            try
            {
                using (var transaction = await this.context.Database.BeginTransactionAsync())
                {
                    await this.dbSet.AddRangeAsync(entities);
                    await this.context.SaveChangesAsync();
                    transaction.Commit();
                    context.Entry(entities).State = EntityState.Detached;
                    return entities;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public async Task<bool> DeleteAsync(TEntity entity)
        {
            try
            {
                using (var transaction = await this.context.Database.BeginTransactionAsync())
                {
                    this.dbSet.Remove(entity);
                    await this.context.SaveChangesAsync();
                    transaction.Commit();
                    return true;
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                throw;
            }
        }
        public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                using (var transaction = await this.context.Database.BeginTransactionAsync())
                {
                    IEnumerable<TEntity> listItem = this.dbSet.AsNoTracking().Where(predicate).ToList();
                    this.dbSet.RemoveRange(listItem);
                    await this.context.SaveChangesAsync();
                    transaction.Commit();
                    return true;
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                throw;
            }
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(int page = 0, int take = 0)
        {
            try
            {
                IQueryable<TEntity> listEntity =  this.dbSet;
                if (take > 0)
                {
                    return await listEntity.AsNoTracking().Skip(page * take).Take(take).ToListAsync();
                }
                return await listEntity.AsNoTracking().ToListAsync();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                throw;
            }
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, int page = 0, int take = 0)
        {
            try
            {
                IQueryable<TEntity> listEntity =  this.dbSet.Where(predicate);
                if (take > 0)
                {
                    return await listEntity.AsNoTracking().Skip(page * take).Take(take).ToListAsync();
                }
                return await listEntity.AsNoTracking().ToListAsync();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                throw;
            }
        }
        public async Task<TEntity> GetAsync(object id)
        {
            try
            {
                TEntity entity = await this.dbSet.FindAsync(id);
                if (entity == null)
                {
                    return new TEntity();
                }
                context.Entry(entity).State = EntityState.Detached;
                return entity;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                throw;
            }
        }

        public async Task<TEntity> GetAsync(params object[] ids)
        {
            try
            {
                TEntity entity = await this.dbSet.FindAsync(ids);
                if (entity == null)
                {
                    return new TEntity();
                }
                context.Entry(entity).State = EntityState.Detached;
                return entity;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                throw;
            }
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                TEntity entity = await this.dbSet.FirstOrDefaultAsync(predicate);
                if (entity == null)
                {
                    return new TEntity();
                }
                context.Entry(entity).State = EntityState.Detached;
                return entity;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                throw;
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            using (var transaction = await this.context.Database.BeginTransactionAsync())
            {
                try
                {
                    var entry = this.context.Entry(entity);
                    dbSet.Attach(entity);
                    entry.State = EntityState.Modified;
                    await this.context.SaveChangesAsync();
                    transaction.Commit();
                    context.Entry(entity).State = EntityState.Detached;
                    return entity;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
        }

        public async Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities)
        {
            using (var transaction = await this.context.Database.BeginTransactionAsync())
            {
                try
                {
                    dbSet.AttachRange(entities);
                    this.context.Entry(entities).State = EntityState.Modified;
                    await this.context.SaveChangesAsync();
                    transaction.Commit();
                    this.context.Entry(entities).State = EntityState.Detached;
                    return entities;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
        }

        public TEntity Get(object id)
        {
            try
            {
                TEntity entity = this.dbSet.Find(id);
                if (entity == null)
                {
                    return new TEntity();
                }
                context.Entry(entity).State = EntityState.Detached;
                return entity;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                throw;
            }
        }

        public TEntity Get(params object[] ids)
        {
            try
            {
                TEntity entity = this.dbSet.Find(ids);
                if (entity == null)
                    return new TEntity();
                context.Entry(entity).State = EntityState.Detached;
                return entity;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                throw;
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                TEntity entity = this.dbSet.FirstOrDefault(predicate);
                if (entity == null)
                {
                    return new TEntity();
                }
                context.Entry(entity).State = EntityState.Detached;
                return entity;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                throw;
            }
        }

        public IEnumerable<TEntity> GetAll(int page = 0, int take = 0)
        {
            try
            {
                IQueryable<TEntity> listEntity = this.dbSet;
                if (take > 0)
                {
                    return listEntity.Skip(page * take).Take(take).AsNoTracking().ToList();
                }
                return listEntity.AsNoTracking().ToList();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                throw;
            }
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, int page = 0, int take = 0)
        {
            try
            {
                IQueryable<TEntity> listEntity = this.dbSet.Where(predicate);
                if (take > 0)
                {
                    return listEntity.Skip(page * take).Take(take).AsNoTracking().ToList();
                }
                return listEntity.AsNoTracking().ToList();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                throw;
            }
        }

        public TEntity Add(TEntity entity)
        {
            try
            {
                using (var transaction = this.context.Database.BeginTransaction())
                {
                    this.dbSet.Add(entity);
                    this.context.SaveChanges();
                    transaction.Commit();
                    context.Entry(entity).State = EntityState.Detached;
                    return entity;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public IEnumerable<TEntity> Add(IEnumerable<TEntity> entities)
        {
            try
            {
                using (var transaction = this.context.Database.BeginTransaction())
                {
                    this.dbSet.AddRange(entities);
                    this.context.SaveChanges();
                    transaction.Commit();
                    context.Entry(entities).State = EntityState.Detached;
                    return entities;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public TEntity Update(TEntity entity)
        {
            using (var transaction = this.context.Database.BeginTransaction())
            {
                try
                {
                    var entry = this.context.Entry(entity);
                    dbSet.Attach(entity);
                    entry.State = EntityState.Modified;
                    this.context.SaveChanges();
                    transaction.Commit();
                    entry.State = EntityState.Detached;
                    return entity;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
        }

        public IEnumerable<TEntity> Update(IEnumerable<TEntity> entities)
        {
            using (var transaction = this.context.Database.BeginTransaction())
            {
                try
                {
                    this.context.AttachRange(entities);
                    this.context.Entry(entities).State = EntityState.Modified;
                    this.context.SaveChanges();
                    transaction.Commit();
                    this.context.Entry(entities).State = EntityState.Detached;
                    return entities;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
        }


        public bool Delete(TEntity entity)
        {
            try
            {
                using (var transaction = this.context.Database.BeginTransaction())
                {
                    this.dbSet.Remove(entity);
                    this.context.SaveChanges();
                    transaction.Commit();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public bool Delete(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                using (var transaction = this.context.Database.BeginTransaction())
                {
                    IEnumerable<TEntity> listItem = this.dbSet.AsNoTracking().Where(predicate);
                    this.dbSet.RemoveRange(listItem);
                    this.context.SaveChanges();
                    transaction.Commit();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public T GetAs<T>(params object[] id) where T:class, new()
        {
            try
            {
                var entity = dbSet.Find(id);
                if (entity != null)
                {
                    context.Entry(entity).State = EntityState.Detached;
                    return entity as T;
                }
                return new T();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public T GetAs<T>(Expression<Func<TEntity, bool>> predicate) where T : class, new()
        {
            try
            {
                var entity = dbSet.FirstOrDefault(predicate);
                if (entity != null)
                {
                    context.Entry(entity).State = EntityState.Detached;
                    return entity as T;
                }
                return new T();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public IEnumerable<T> GetAllAs<T>(Expression<Func<TEntity, bool>> predicate, int page = 0, int take = 0) where T : class, new()
        {
            try
            {
                IQueryable<TEntity> listEntity = this.dbSet.Where(predicate);
                if (take > 0)
                {
                    return listEntity.Skip(page * take).Take(take).Cast<T>().AsNoTracking().ToList();
                }
                return listEntity.AsNoTracking().Cast<T>().ToList();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                throw;
            }
        }

        public IEnumerable<T> GetAllAs<T>(int page = 0, int take = 0) where T : class, new()
        {
            try
            {
                IQueryable<TEntity> listEntity = this.dbSet;
                if (take > 0)
                {
                    return listEntity.Skip(page * take).Take(take).Cast<T>().AsNoTracking().ToList();
                }
                return listEntity.AsNoTracking().Cast<T>().ToList();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                throw;
            }
        }

        public async Task<T> GetAsyncAs<T>(params object[] id) where T : class, new()
        {
            try
            {
                var entity = await dbSet.FindAsync(id);
                if (entity != null)
                {
                    context.Entry(entity).State = EntityState.Detached;
                    return entity as T;
                }
                return new T();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<T> GetAsyncAs<T>(Expression<Func<TEntity, bool>> predicate) where T : class, new()
        {
            try
            {
                var entity = await dbSet.FirstOrDefaultAsync(predicate);
                if (entity != null)
                {
                    context.Entry(entity).State = EntityState.Detached;
                    return entity as T;
                }
                return new T();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsyncAs<T>(Expression<Func<TEntity, bool>> predicate, int page = 0, int take = 0) where T : class, new()
        {
            try
            {
                IQueryable<TEntity> listEntity = this.dbSet.Where(predicate);
                if (take > 0)
                {
                    return await listEntity.Skip(page * take).Take(take).Cast<T>().AsNoTracking().ToListAsync();
                }
                return await listEntity.AsNoTracking().Cast<T>().ToListAsync();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                throw;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsyncAs<T>(int page = 0, int take = 0) where T : class, new()
        {
            try
            {
                IQueryable<TEntity> listEntity = this.dbSet;
                if (take > 0)
                {
                    return await listEntity.Skip(page * take).Take(take).Cast<T>().AsNoTracking().ToListAsync();
                }
                return await listEntity.AsNoTracking().Cast<T>().ToListAsync();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                throw;
            }
        }
    }
}
