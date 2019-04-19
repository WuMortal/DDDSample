using System;
using System.Threading.Tasks;
using FreeSql;
using Wigor.Sample.Domain.Entity;

namespace Wigor.Sample.Domain.IRepository
{
    public interface IUserRepository : IBasicRepository<UserEntity, Guid>
    {
        Task<UserEntity> GetByMobileAsync(string mobile);
    }
}
