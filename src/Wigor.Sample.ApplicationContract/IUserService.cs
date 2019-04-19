using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
    }
}
