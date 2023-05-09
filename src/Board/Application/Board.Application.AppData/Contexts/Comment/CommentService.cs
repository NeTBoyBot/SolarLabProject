using AutoMapper;
using Board.Application.AppData.Contexts.Comment;
using Board.Contracts.Comment;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        public readonly ILogger<CommentService> _logger;

        public CommentService(ICommentRepository commentRepository, IMapper mapper, ILogger<CommentService> logger)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Guid> CreateCommentAsync(Guid userId,CreateCommentRequest createChat,CancellationToken cancellation)
        {
            _logger.LogInformation($"Создание комментария");

            var newComment = _mapper.Map<Board.Domain.Comment>(createChat);
            newComment.UserId = userId;
            //newChat.Id = Guid.NewGuid();
            await _commentRepository.AddAsync(newComment,cancellation);

            return newComment.Id;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellation)
        {
            _logger.LogInformation($"Удаление комментария под id {id}");

            var existingChat = await _commentRepository.FindById(id,cancellation);
            await _commentRepository.DeleteAsync(existingChat,cancellation);
        }

        public async Task<IReadOnlyCollection<InfoCommentResponse>> GetAll(int take, int skip)
        {
            _logger.LogInformation($"Получение всех комментариев");

            return await _commentRepository.GetAll()
                .Select(a => new InfoCommentResponse
                {
                    Id = a.Id,
                    UserId = a.UserId,
                    Text = a.Text,
                    SenderId = a.SenderId
                }).OrderBy(a => a.Id).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<ICollection<InfoCommentResponse>> GetAllCommentsForUser(Guid userId, CancellationToken cancellation)
        {
            _logger.LogInformation($"Получение всех комментариев пользователя под id {userId}");

            return await _commentRepository.GetAll().Where(a=>a.UserId == userId)
                .Select(a => new InfoCommentResponse
                {
                    Id = a.Id,
                    UserId = a.UserId,
                    Text = a.Text,
                    SenderId = a.SenderId
                }).OrderBy(a => a.Id).ToListAsync();
        }

        public async Task<InfoCommentResponse> GetByIdAsync(Guid id, CancellationToken cancellation)
        {
            _logger.LogInformation($"Получение комментария под id {id}");

            var existingchat = await _commentRepository.FindById(id,cancellation);
            return _mapper.Map<InfoCommentResponse>(existingchat);
        }
    }
}
