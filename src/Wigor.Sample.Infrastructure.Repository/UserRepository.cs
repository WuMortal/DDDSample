using FreeSql;
using System;
using System.Collections.Generic;
using System.Text;
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

    }
}
