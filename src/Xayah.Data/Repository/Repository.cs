using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xayah.Business.Interface;
using Xayah.Business.Model;
using Xayah.Data.Context;

namespace Xayah.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly XayahContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(XayahContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate)
        {
            //Acess a Db for a specific entitie where expression return async list
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }
        public virtual async Task<TEntity> GetById(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await DbSet.ToListAsync();
        }
        public virtual async Task Add(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public List<Transaction> GetAllTransactions()
        {
            var transactions = (from t in Db.Transactions
                                orderby 1 descending
                                select new Transaction
                                {
                                    TRNTYPE = t.TRNTYPE,
                                    DTPOSTED = t.DTPOSTED,
                                    TRNAMT = t.TRNAMT,
                                    MEMO = t.MEMO


                                });
            return transactions.Distinct().ToList();
        }

        public virtual async Task Update(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Remove(int id)
        {
            DbSet.Remove(new TEntity { Id = id });
            await SaveChanges();           
        }

        public virtual async Task RemoveAll()
        {
            DbSet.RemoveRange(DbSet);
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
