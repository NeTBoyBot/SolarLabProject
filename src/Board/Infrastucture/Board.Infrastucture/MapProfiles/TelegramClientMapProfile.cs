using AutoMapper;
using Board.Contracts.TelegramClient;
using Board.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Infrastucture.MapProfiles
{
    public class TelegramClientMapProfile : Profile
    {
        public TelegramClientMapProfile()
        {
            CreateMap<TelegramClient, InfoTelegramClientResponse>().ReverseMap();
            CreateMap<TelegramClient, CreateTelegramClientRequest>().ReverseMap();
        }
    }
}
