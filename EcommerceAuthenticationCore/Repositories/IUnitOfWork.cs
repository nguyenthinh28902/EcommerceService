using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAuthenticationCore.Repositories
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        Task CommitAsync();
        Task RollbackAsync();
        Task SaveChangeAsync();

    }
}
