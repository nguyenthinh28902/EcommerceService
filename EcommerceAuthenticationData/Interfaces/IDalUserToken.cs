using EcommerceAuthenticationCore.Models;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAuthenticationData.Interfaces
{
    public interface IDalUserToken
    {
        public Task<bool> CreateAsync(UserToken userToken);
        public Task UpdateAsync(UserToken user);
        public Task<UserToken> GetFirstOrDefaultAsync(Expression<Func<UserToken, bool>> predicate = null,
            Func<IQueryable<UserToken>, IOrderedQueryable<UserToken>> orderBy = null,
            Func<IQueryable<UserToken>, IIncludableQueryable<UserToken, object>> include = null);
    }
}
