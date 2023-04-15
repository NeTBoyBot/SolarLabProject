using AutoMapper;
using Board.Application.AppData.Contexts.Message;
using Board.Contracts.Message;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doska.AppServices.Services.Message
{
    public class MessageService : IMessageService
    {
        public readonly IMessageRepository _messageRepository;
        public readonly IMapper _mapper;

        public MessageService(IMessageRepository messageRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        public async Task<Guid> CreateMessageAsync(CreateMessageRequest createMessage, CancellationToken cancellation)
        {
            var newMessage = _mapper.Map<Board.Domain.Message>(createMessage);
            await _messageRepository.AddAsync(newMessage,cancellation);

            return newMessage.Id;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellation)
        {
            var existingMessage = await _messageRepository.FindById(id,cancellation);
            await _messageRepository.DeleteAsync(existingMessage,cancellation);
        }

        public async Task<IReadOnlyCollection<InfoMessageResponse>> GetAll(int take, int skip)
        {
            return await _messageRepository.GetAll()
                .Select(a => new InfoMessageResponse
                {
                    Id = a.Id,
                    ChatId = a.ChatId,
                    Containment = a.Containment
                }).OrderBy(a => a.Id).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<IReadOnlyCollection<string>> GetAllInChat(Guid ChatId, CancellationToken cancellation)
        {
            return await _messageRepository.GetAll().Where(m => m.ChatId == ChatId)
                .Select(a=>a.Containment).OrderBy(a => a).ToListAsync();
        }

        public async Task<InfoMessageResponse> GetByIdAsync(Guid id,CancellationToken cancellation)
        {
            var existingchat = await _messageRepository.FindById(id,cancellation);
            return _mapper.Map<InfoMessageResponse>(existingchat);
        }
    }
}
