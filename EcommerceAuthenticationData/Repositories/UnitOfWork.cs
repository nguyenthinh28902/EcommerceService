using EcommerceAuthenticationCore.Models;
using EcommerceAuthenticationCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAuthenticationData.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed;
        private string _errorMessage = string.Empty;
        private IDbContextTransaction _contextTransaction;
        public EcommerceAuthenticationServiceContext _context { get; }
        private Dictionary<Type, object> _repositories;
        public UnitOfWork(EcommerceAuthenticationServiceContext context)
        {
            _context = context;
            _contextTransaction = _context.Database.BeginTransaction();
            _repositories = new Dictionary<Type, object>();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories.ContainsKey(typeof(TEntity)))
            {
                return (IRepository<TEntity>)_repositories[typeof(TEntity)];
            }

            var repository = new Repository<TEntity>(_context);
            _repositories.Add(typeof(TEntity), repository);
            return repository;
        }


        public async Task CommitAsync()
        {
          await _contextTransaction.CommitAsync();
        }

        public async void Dispose()
        {
            await Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task RollbackAsync()
        {
            await _contextTransaction.RollbackAsync();
            await _contextTransaction.DisposeAsync();
        }

        public async Task SaveChangeAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is Microsoft.EntityFrameworkCore.DbUpdateException innerException
                  && innerException.InnerException is Microsoft.Data.SqlClient.SqlException sqlException)
                {
                    _errorMessage = $"SQL error occurred: {sqlException.Message} {Environment.NewLine}";
                }
                else
                {
                    _errorMessage = $"Error occurred: {ex.Message}  {Environment.NewLine}";
                }
                throw new Exception(_errorMessage, ex);
            }
        }
        protected virtual async Task Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    await _context.DisposeAsync();
            _disposed = true;
        }
    }
}
