using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookShop_API.BookShop_Repostory.Repostory
{
    public interface IRepository<TEnity> where TEnity : class
    {
        IEnumerable<TEnity> All { get; }
        IQueryable<TEnity> AllQuery { get; }
        void Insert(TEnity entity);
        void Update(TEnity entity);
        void Remove(TEnity entity);
        void RemoveRange(IEnumerable<TEnity> entity);
        IQueryable<TEnity> FindBy(Expression<Func<TEnity, bool>> predicate);
        Task<IEnumerable<TEnity>> AllAsync();
        Task<int> SaveAsync();
        void Save();
        Task<IDbContextTransaction> BeginTransactionAsync();
        IDbContextTransaction BeginTransaction();
        //IDbContextTransaction UseTransaction(IDbContextTransaction transaction);
        void CommitTransaction(IDbContextTransaction transaction);
        void RollBackTransaction(IDbContextTransaction transaction);
    }
}
