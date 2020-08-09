using System.Collections.Generic;

namespace Northwind.DAL.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
         void Add(TEntity entity);

         void Delete(TEntity entity);

         TEntity Get(int id);

         IEnumerable<TEntity> GetAll();
    }
}