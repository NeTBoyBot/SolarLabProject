using AutoMapper;
using Board.Contracts.Photo.AdPhoto;
using Board.Domain.Photos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Infrastucture.MapProfiles.Photos
{
    public class UserPhotoProfile : Profile
    {
        public UserPhotoProfile()
        {
            CreateMap<UserPhoto, InfoUserPhotoResponse>().ReverseMap();
            CreateMap<UserPhoto, CreateUserPhotoRequest>().ReverseMap();
        }
    }
}
