using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Board.Contracts.User;
using Board.Domain;

namespace Doska.AppServices.MapProfile
{
    public class UserMapProfile : Profile
    {
        public UserMapProfile()
        {
            CreateMap<User, InfoUserResponse>().ReverseMap();
            CreateMap<User, LoginUserRequest>().ReverseMap();
            CreateMap<User, RegisterUserRequest>().ReverseMap();
        }
    }
}
