using FreeSql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wigor.Sample.Domain.Entity;
using Wigor.Sample.Domain.IRepository;

namespace Wigor.Sample.Infrastructure.Repository
{
    public class UserRepository : GuidRepository<UserEntity>, IUserRepository
    {
        public UserRepository(IFreeSql freeSql)
            : base(freeSql)
        {
        }


        #region Implementation of IUserRepository

        public async Task<UserEntity> GetByMobileAsync(string mobile)
        {
            return await this.Where(u => u.Mobile == mobile).FirstAsync();
        }

        #endregion
    }
}
