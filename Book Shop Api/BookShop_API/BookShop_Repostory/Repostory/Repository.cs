using BookShop_API.BookShop_Repostory.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookShop_API.BookShop_Repostory.Repostory
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly IdentityAppContext _dbContext;
        private readonly DbSet<TEntity> _entity;
        public Repository(IdentityAppContext context)
        {
            _dbContext = context;
            _entity = _dbContext.Set<TEntity>();
        }

        public IDbContextTransaction BeginTransaction()
        {
            
            return _dbContext.Database.BeginTransaction();
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _dbContext.Database.BeginTransactionAsync();
        }
       
        public void CommitTransaction(IDbContextTransaction transaction)
        {
            transaction.Commit();
        }
        public void RollBackTransaction(IDbContextTransaction transaction)
        {
            transaction.Rollback();
        }
        public IEnumerable<TEntity> All
        {
            get { return _entity; }
        }
        public IQueryable<TEntity> AllQuery
        {
            get { return _entity; }
        }
        public async Task<IEnumerable<TEntity>> AllAsync()
        {
            return await _entity.ToListAsync();
        }

        public void Insert(TEntity entity)
        {
            _entity.Add(entity);
        }

        public void Remove(TEntity entity)
        {
            _entity.Remove(entity);
        }
        public void RemoveRange(IEnumerable<TEntity> entity)
        {
            _entity.RemoveRange(entity);
        }

        public void Update(TEntity entity)
        {
            _entity.Update(entity);
        }
        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return ((IQueryable<TEntity>)_entity).Where(predicate);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
