using EcommerceAuthenticationCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAuthenticationCore.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly EcommerceAuthenticationServiceContext _context;
        private DbSet<T> _dbSet;
        
        public Repository(EcommerceAuthenticationServiceContext context)
        {
           
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public Task DeleteAsync(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            var entity = await _dbSet.FindAsync(id);
            return entity;
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
