using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Wigor.Sample.ApplicationContract.DTO;
using Wigor.Sample.Domain.Entity;

namespace Wigor.Sample.Application.Map
{
    public class AutoMapProfile : Profile
    {
        public AutoMapProfile()
        {
            CreateMap<UserEntity, UserDTO>();
        }
    }
}
