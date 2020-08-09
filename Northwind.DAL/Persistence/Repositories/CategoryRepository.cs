using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Northwind.DAL.Core;
using Northwind.DAL.Core.Domain;

namespace Northwind.DAL.Persistence.Repositories
{
    public class CategoryRepository : Repository,  ICategoryRepository
    {
        public CategoryRepository(IDbTransaction transaction) 
            : base(transaction)
        {
        }

        public void Add(Category entity)
        {
             if (entity == null)
                throw new ArgumentNullException("entity");
                
            
            entity.CategoryId = Connection.ExecuteScalar<int>(
                "INSERT INTO Categories (CategoryName, Description) VALUES(@categoryName, @description); SELECT SCOPE_IDENTITY()",
                param: new { categoryName = entity.CategoryName, Description = entity.Description },
                transaction: Transaction
            );
        }

        public void Delete(Category entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            Connection.Execute(
                "DELETE FROM Categories WHERE CategoryId = @categoryId",
                param: new { categoryId = entity.CategoryId },
                transaction: Transaction
            );
        }

        public Category Get(int id)
        {
            return  Connection.Query<Category>("SELECT CategoryId, CategoryName, Description FROM Categories WHERE CategoryId = @categoryId",
                param: new { categoryId = id },
                transaction: Transaction).FirstOrDefault();
        }

        public IEnumerable<Category> GetAll()
        {
            return Connection.Query<Category>("SELECT CategoryId, CategoryName, Description FROM Categories", transaction: Transaction);
        }
    }
}