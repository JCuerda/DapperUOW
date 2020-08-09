using System;
using System.Data;
using System.Data.SqlClient;
using Northwind.DAL.Core;
using Northwind.DAL.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private IDbConnection _connection;
        
    private IDbTransaction _transaction;

     private bool _disposed;
        
    public ICategoryRepository CategoryRepository { get; private set; }

    public UnitOfWork(string connectionString)
    {
        _connection = new SqlConnection(connectionString);
        // _connection.Open();
        // _transaction = _connection.BeginTransaction();
    }

    public void Begin()
    {
        _connection.Open();
        _transaction = _connection.BeginTransaction();
        CategoryRepository = new CategoryRepository(_transaction);
    }

    public void Commit()
    {
        try
        {
            _transaction.Commit();
        }
        catch
        {
            _transaction.Rollback();
            throw;
        }
        finally
        {
            _transaction.Dispose();
            _transaction = _connection.BeginTransaction();
            ResetRepositories();
        }
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if(disposing)
            {
                if (_transaction != null)
                {
                    _transaction.Dispose();
                    _transaction = null;
                }
                if(_connection != null)
                {
                    _connection.Dispose();
                    _connection = null;
                }
            }
            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void ResetRepositories()
    {
        CategoryRepository = null;
    }

}