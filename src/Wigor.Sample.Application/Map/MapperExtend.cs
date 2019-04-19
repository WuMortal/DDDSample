using System;
using System.Collections.Generic;
using System.Text;
using Wigor.Sample.ApplicationContract.DTO;
using Wigor.Sample.Domain.Entity;

namespace Wigor.Sample.Application.Map
{
    public static class MapperExtend
    {
        public static List<UserDTO> ToDTOList(this IList<UserEntity> value)
        {
            if (value == null)
                return new List<UserDTO>();

            return AutoMapper.Mapper.Map<List<UserDTO>>(value);
        }

        public static UserDTO ToDTO(this UserEntity value)
        {
            if (value == null)
                return null;

            return AutoMapper.Mapper.Map<UserDTO>(value);
        }
    }
}
