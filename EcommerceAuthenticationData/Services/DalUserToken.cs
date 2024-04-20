using EcommerceAuthenticationCore.Models;
using EcommerceAuthenticationCore.Repositories;
using EcommerceAuthenticationData.Interfaces;
using EcommerceAuthenticationData.Models;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAuthenticationData.Services
{
    public class DalUserToken : IDalUserToken
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<UserToken> _userRepository;
        private readonly ILogger<DalUserToken> _logger;
        public DalUserToken(IUnitOfWork unitOfWork, ILogger<DalUserToken> logger)
        {
            _unitOfWork = unitOfWork;
            _userRepository = unitOfWork.GetRepository<UserToken>();
            _logger = logger;
        }

        public async Task<bool> CreateAsync(UserToken userToken)
        {
            try
            {
                await _userRepository.AddAsync(userToken);
                await _unitOfWork.CommitAsync();
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                var informationLoger = new InformationLoger(nameof(CreateAsync), ex, userToken);
                _logger.LogError(informationLoger.GetMessage());
                return false;
            }
            return true;
        }
        public async Task UpdateAsync(UserToken user)
        {
            try
            {
                _userRepository.Update(user);
                await _unitOfWork.CommitAsync();
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
            }
        }
        public async Task<UserToken> GetFirstOrDefaultAsync(Expression<Func<UserToken, bool>> predicate = null,
          Func<IQueryable<UserToken>, IOrderedQueryable<UserToken>> orderBy = null,
          Func<IQueryable<UserToken>, IIncludableQueryable<UserToken, object>> include = null)
        {
            var userToken = await _userRepository.GetFirstOrDefaultAsync(predicate, orderBy, include);
            return userToken;
        }
    }
}
