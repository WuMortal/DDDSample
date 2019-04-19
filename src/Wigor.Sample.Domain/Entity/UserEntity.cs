using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Wigor.Sample.Domain.Entity
{
    public class UserEntity
    {
        [Column(IsIdentity = true)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public DateTime RegisterDateTime { get; set; }
    }
}
