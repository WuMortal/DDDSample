using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Wigor.Sample.Domain.Entity;
using Wigor.Sample.Infrastructure.Repository;

namespace Wigor.Sample.Infrastructure.RepositoryTest
{
    [TestClass]
    public class UserRepositoryUnitTest
    {

        [TestMethod]
        public void TestAddMethod_Success()
        {
            using (var userRepository = new UserRepository(DBInit.DBFreeSql))
            {
                var id = Guid.NewGuid();
                userRepository.Insert(new UserEntity
                {
                    Id = id
                });

                var userEntity = userRepository.Get(id);

                Assert.IsTrue(id == userEntity.Id);
            }
        }
    }
}
