using AutoMapper;
using Board.Contracts.Message;
using Board.Contracts.Role;
using Board.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Infrastucture.MapProfiles
{
    public class RoleMapProfile : Profile
    {
        public RoleMapProfile()
        {
            CreateMap<Role, InfoRoleResponse>().ReverseMap();
        }
    }
}
