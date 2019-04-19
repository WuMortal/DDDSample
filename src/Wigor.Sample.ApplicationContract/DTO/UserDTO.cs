using System;
using System.Collections.Generic;
using System.Text;

namespace Wigor.Sample.ApplicationContract.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Mobile { get; set; }

        public int Age { get; set; }

        public string ProfilePhotoSrc { get; set; }
    }
}
