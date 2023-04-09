using AutoMapper;
using Board.Contracts.Message;
using Board.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doska.AppServices.MapProfile
{
    public class MessageMapProfile : Profile
    {
        public MessageMapProfile()
        {
            CreateMap<Message, InfoMessageResponse>().ReverseMap();
            CreateMap<Message, CreateMessageRequest>().ReverseMap();
        }
    }
}
