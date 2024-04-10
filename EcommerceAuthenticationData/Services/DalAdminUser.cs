using EcommerceAuthenticationCore.Models;
using EcommerceAuthenticationCore.Repositories;
using EcommerceAuthenticationData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAuthenticationData.Services
{
    public class DalAdminUser : IDalAdminUser
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<AdminUser> _userRepository;

        public DalAdminUser(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
            _userRepository = unitOfWork.GetRepository<AdminUser>();
        }
        public async Task<AdminUser> CreateAsync(AdminUser user)
        {
            try
            {
                await _userRepository.AddAsync(user);
                await _unitOfWork.CommitAsync();
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception)
            {
                user.Id = 0;
                await _unitOfWork.RollbackAsync();
            }
            return user;
        }

      
        public async Task<bool> DeleteleAsync(object id)
        {
            try
            {
                await _userRepository.DeleteAsync(id);
                await _unitOfWork.CommitAsync();
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception)
            {
                return true;
            }
            return true;
        }

        public async Task<AdminUser> UpdateAsync(AdminUser user)
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
            return user;
        }
        public async Task<AdminUser?> GetUserByIdAsync(object id)
        {
           var user = await _userRepository.GetByIdAsync(id);
            return user;
        }
    }
}
