using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wigor.Sample.Application.Map;
using Wigor.Sample.ApplicationContract;
using Wigor.Sample.ApplicationContract.DTO;
using Wigor.Sample.Domain.Entity;
using Wigor.Sample.Domain.IRepository;

namespace Wigor.Sample.Application.Service
{
    public class UserService : IUserService
    {
        readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #region Implementation of IUserService

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="mobile">手机</param>
        /// <param name="age">年龄</param>
        /// <returns></returns>
        public async Task<bool> Register(string userName, string mobile, int age)
        {
            var userEnity = await _userRepository.GetByMobileAsync(mobile);

            if (userEnity != null)
            {
                return false;
            }

            var addUserEntity = new UserEntity
            {
                Id = Guid.NewGuid(),
                Age = age,
                Name = userName,
                Mobile = mobile
            };

            return await _userRepository.InsertAsync(addUserEntity) != null;
        }

        public List<UserDTO> GetList()
        {
            return _userRepository.Select
                .ToList().ToDTOList();
        }

        #endregion
    }
}
