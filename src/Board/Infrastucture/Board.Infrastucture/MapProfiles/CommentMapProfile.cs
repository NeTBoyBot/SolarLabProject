using AutoMapper;
using Board.Contracts.Comment;
using Board.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doska.AppServices.MapProfile
{
    public class CommentMapProfile : Profile
    {
        public CommentMapProfile()
        {
            CreateMap<Comment, InfoCommentResponse>().ReverseMap();
            CreateMap<Comment, CreateCommentRequest>().ReverseMap();
        }
    }
}
