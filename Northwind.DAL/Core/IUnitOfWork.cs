using System;
using Northwind.DAL.Persistence.Repositories;

namespace Northwind.DAL.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository CategoryRepository { get; }

        void Begin();

        void Commit();
    }
}