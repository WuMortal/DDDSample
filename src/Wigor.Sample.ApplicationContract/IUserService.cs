using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wigor.Sample.ApplicationContract.DTO;

namespace Wigor.Sample.ApplicationContract
{
    public interface IUserService
    {
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="age">年龄</param>
        /// <returns></returns>
        Task<bool> Register(string userName, int age);

        List<UserDTO> GetList();
    }
}
