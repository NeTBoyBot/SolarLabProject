using AutoMapper;
using Board.Contracts.FavoriteAd;
using Board.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doska.AppServices.MapProfile
{
    public class FavoriteAdMapProfile : Profile
    {
        public FavoriteAdMapProfile()
        {
            CreateMap<FavoriteAd, CreateFavoriteAdRequest>().ReverseMap();
            CreateMap<FavoriteAd, InfoFavoriteAdResponse>().ReverseMap();
        }
    }
}
