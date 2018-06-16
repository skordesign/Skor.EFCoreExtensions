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
                    context.Entry(entity).State = EntityState.Detached;
                    return entity;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }
        public async Task<IEnumerable<TEntity>> AddAsync(IEnumerable<TEntity> entity)
        {
            try
            {
                using (var transaction = await this.context.Database.BeginTransactionAsync())
                {
                    foreach (TEntity item in entity)
                    {
                        await this.dbSet.AddAsync(item);
                    }
                    await this.context.SaveChangesAsync();
                    transaction.Commit();
                    context.Entry(entity).State = EntityState.Detached;
                    return entity;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
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
                return false;
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
                return false;
            }
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(int page = 0, int take = 0)
        {
            try
            {
                IQueryable<TEntity> listEntity =  this.dbSet;
                if (take > 0)
                {
                    return listEntity.Skip(page * take).Take(take);
                }
                return await listEntity.AsNoTracking().ToListAsync();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                throw err;
            }
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, int page = 0, int take = 0)
        {
            try
            {
                IQueryable<TEntity> listEntity =  this.dbSet.Where(predicate);
                if (take > 0)
                {
                    return listEntity.Skip(page * take).Take(take);
                }
                return await listEntity.AsNoTracking().ToListAsync();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                throw err;
            }
        }
        public async Task<TEntity> GetAsync(object id)
        {
            try
            {
                TEntity entity = await this.dbSet.FindAsync(id);
                context.Entry(entity).State = EntityState.Detached;
                return entity;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return new TEntity();
            }
        }

        public async Task<TEntity> GetAsync(params object[] ids)
        {
            try
            {
                TEntity entity = await this.dbSet.FindAsync(ids.Cast<object>().ToArray());
                context.Entry(entity).State = EntityState.Detached;
                return entity;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return new TEntity();
            }
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                TEntity entity = await this.dbSet.FirstOrDefaultAsync(predicate);
                context.Entry(entity).State = EntityState.Detached;
                return entity;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return new TEntity();
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
                    return new TEntity();
                }
            }
        }

        public async Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities)
        {
            using (var transaction = await this.context.Database.BeginTransactionAsync())
            {
                try
                {
                    foreach (var entity in entities)
                    {
                        var entry = this.context.Entry(entity);
                        dbSet.Attach(entity);
                        entry.State = EntityState.Modified;
                    }
                    await this.context.SaveChangesAsync();
                    transaction.Commit();
                    foreach (var entity in entities)
                    {
                        var entry = this.context.Entry(entity);
                        dbSet.Attach(entity);
                        entry.State = EntityState.Detached;
                    }
                    return entities;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return new List<TEntity>();
                }
            }
        }


        public TEntity Get(object id)
        {
            try
            {
                TEntity entity = this.dbSet.Find(id);
                context.Entry(entity).State = EntityState.Detached;
                return entity;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return new TEntity();
            }
        }

        public TEntity Get(params object[] ids)
        {
            try
            {
                TEntity entity = this.dbSet.Find(ids.Cast<object>().ToArray());
                context.Entry(entity).State = EntityState.Detached;
                return entity;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return new TEntity();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                TEntity entity = this.dbSet.FirstOrDefault(predicate);
                return entity;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return new TEntity();
            }
        }

        public IEnumerable<TEntity> GetAll(int page = 0, int take = 0)
        {
            try
            {
                IQueryable<TEntity> listEntity = this.dbSet;
                if (take > 0)
                {
                    return listEntity.Skip(page * take).Take(take);
                }
                return listEntity.AsNoTracking().ToList();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return new List<TEntity>();
            }
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, int page = 0, int take = 0)
        {
            try
            {
                IQueryable<TEntity> listEntity = this.dbSet.Where(predicate);
                if (take > 0)
                {
                    return listEntity.Skip(page * take).Take(take);
                }
                return listEntity.AsNoTracking().ToList();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return new List<TEntity>();
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
                throw e;
            }
        }

        public IEnumerable<TEntity> Add(IEnumerable<TEntity> entity)
        {
            try
            {
                using (var transaction = this.context.Database.BeginTransaction())
                {
                    foreach (TEntity item in entity)
                    {
                        this.dbSet.Add(item);
                    }
                    this.context.SaveChanges();
                    transaction.Commit();
                    foreach (TEntity item in entity)
                    {
                        context.Entry(entity).State = EntityState.Detached;
                    }
                    return entity;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
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
                    throw e;
                }
            }
        }

        public IEnumerable<TEntity> Update(IEnumerable<TEntity> entities)
        {
            using (var transaction = this.context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var entity in entities)
                    {
                        var entry = this.context.Entry(entity);
                        entry.State = EntityState.Detached;
                        dbSet.Attach(entity);
                        entry.State = EntityState.Modified;
                    }
                    this.context.SaveChanges();
                    transaction.Commit();
                    foreach (var entity in entities)
                    {
                        var entry = this.context.Entry(entity);
                        dbSet.Attach(entity);
                        entry.State = EntityState.Detached;
                    }
                    return entities;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw e;
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
                throw e;
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
                throw e;
            }
        }
    }
}
