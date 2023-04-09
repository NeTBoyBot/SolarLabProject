using AutoMapper;
using Board.Contracts.Comment;
using Doska.AppServices.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doska.AppServices.Services.Comment
{
    public class CommentService : ICommentService
    {
        public readonly ICommentRepository _commentRepository;
        public readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<Guid> CreateCommentAsync(CreateCommentRequest createChat,CancellationToken cancellation)
        {
            var newChat = _mapper.Map<Board.Domain.Comment>(createChat);
            //newChat.Id = Guid.NewGuid();
            await _commentRepository.AddAsync(newChat,cancellation);

            return newChat.Id;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellation)
        {
            var existingChat = await _commentRepository.FindById(id,cancellation);
            await _commentRepository.DeleteAsync(existingChat,cancellation);
        }

        public async Task<IReadOnlyCollection<InfoCommentResponse>> GetAll(int take, int skip)
        {
            return await _commentRepository.GetAll()
                .Select(a => new InfoCommentResponse
                {
                    Id = a.Id,
                    UserId = a.UserId,
                    Text = a.Text
                }).OrderBy(a => a.Id).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<ICollection<InfoCommentResponse>> GetAllCommentsForUser(Guid userId, CancellationToken cancellation)
        {
            return await _commentRepository.GetAll().Where(a=>a.UserId == userId)
                .Select(a => new InfoCommentResponse
                {
                    Id = a.Id,
                    UserId = a.UserId,
                    Text = a.Text
                }).OrderBy(a => a.Id).ToListAsync();
        }

        public async Task<InfoCommentResponse> GetByIdAsync(Guid id, CancellationToken cancellation)
        {
            var existingchat = await _commentRepository.FindById(id,cancellation);
            return _mapper.Map<InfoCommentResponse>(existingchat);
        }
    }
}
