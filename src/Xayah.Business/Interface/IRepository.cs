﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xayah.Business.Model;

namespace Xayah.Business.Interface
{
   public interface IRepository<TEntity> :IDisposable where TEntity : Entity
    {
        Task Add(TEntity entity);

        Task<TEntity> GetById(int id);

        Task<List<TEntity>> GetAll();

        Task Update(TEntity entity);

        Task Remove(int id);

        Task RemoveAll();

        Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate);

        Task<int> SaveChanges();
    }
}
