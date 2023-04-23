using AutoMapper;
using Board.Contracts.Ad;
using Board.Contracts.Photo.AdPhoto;
using Board.Domain;
using Board.Domain.Photos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Infrastucture.MapProfiles.Photos
{
    public class AdPhotoProfile : Profile
    {
        public AdPhotoProfile()
        {
            CreateMap<AdPhoto, InfoAdPhotoResponse>().ReverseMap();
            CreateMap<AdPhoto, CreateAdPhotoRequest>().ReverseMap();
        }
    }
}
