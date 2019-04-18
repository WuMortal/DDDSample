using System;
using System.Collections.Generic;
using System.Text;

namespace Wigor.Sample.Infrastructure.RepositoryTest
{
    public class DBInit
    {
        public static IFreeSql DBFreeSql
        {
            get
            {
                var fsql = new FreeSql.FreeSqlBuilder()
                            .UseConnectionString(FreeSql.DataType.Sqlite, @"Data Source=|DataDirectory|\document.db;Pooling=true;Max Pool Size=10")
                             .UseAutoSyncStructure(true) //自动迁移实体的结构到数据库
                            .Build();
                return fsql;
            }
        }
    }
}
