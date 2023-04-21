using AutoMapper;
using Board.Contracts.File;
using Board.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Infrastucture.MapProfiles
{
    public class FileMapProfile : Profile
    {
        public FileMapProfile()
        {
            CreateMap<Domain.File, InfoFileResponse>().ReverseMap();
            CreateMap<Domain.File, CreateFileRequest>().ReverseMap();
            
        }
    }
}
