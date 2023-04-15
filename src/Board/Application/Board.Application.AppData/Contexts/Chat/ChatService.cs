using AutoMapper;
using Board.Application.AppData.Contexts.Chat;
using Board.Contracts.Chat;
using Board.Contracts.Message;
using Doska.AppServices.Services.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doska.AppServices.Services.Chat
{
    public class ChatService : IChatService
    {
        public readonly IChatRepository _chatRepository;
        public readonly IMapper _mapper;
        public readonly IUserService _userService;

        public ChatService(IChatRepository chatRepository, IMapper mapper, IUserService userService)
        {
            _chatRepository = chatRepository;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<Guid> CreateChatAsync(CreateChatRequest createChat, CancellationToken cancellation)
        {
            var newChat = _mapper.Map<Board.Domain.Chat>(createChat);
            //newChat.Id = Guid.NewGuid();
            await _chatRepository.AddAsync(newChat,cancellation);

            return newChat.Id;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellation)
        {
            var existingChat = await _chatRepository.FindById(id,cancellation);
            await _chatRepository.DeleteAsync(existingChat,cancellation);
        }

        public async Task<IReadOnlyCollection<InfoChatResponse>> GetAll(int take, int skip)
        {
            return await _chatRepository.GetAll()
                .Select(a => new InfoChatResponse
                {
                    Id = a.Id,
                    Creator = new Board.Contracts.User.InfoUserResponse
                    {
                        UserName = a.Creator.UserName
                    },
                    Member = new Board.Contracts.User.InfoUserResponse
                    {
                        UserName = a.Member.UserName
                    },

                    Messages = a.Messages.Select(s=>new InfoMessageResponse
                    {
                        Id = s.Id,
                        Containment = s.Containment
                    }).ToList()
                }).OrderBy(a => a.Id).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<IReadOnlyCollection<InfoChatResponse>> GetAllChatsForUser(Guid UserId,CancellationToken cancellation)
        {
            return await _chatRepository.GetAll()
                .Where(c=>c.CreatorId == UserId)
                .Select(a => new InfoChatResponse
                {
                    Id = a.Id,
                    Creator = new Board.Contracts.User.InfoUserResponse
                    {
                        UserName = a.Creator.UserName
                    },
                    Member = new Board.Contracts.User.InfoUserResponse
                    {
                        UserName = a.Member.UserName
                    },

                    Messages = a.Messages.Select(s => new InfoMessageResponse
                    {
                        Id = s.Id,
                        Containment = s.Containment
                    }).ToList()
                }).OrderBy(a => a.Id).ToListAsync();
        }

        public async Task<IReadOnlyCollection<InfoChatResponse>> GetAllUserChats(int take, int skip, CancellationToken token)
        {
            var userId = await _userService.GetCurrentUserId(token);
            return await _chatRepository.GetAll().Where(a => a.CreatorId == userId || a.MemberId == userId)
                .Select(a => new InfoChatResponse
                {
                    Id = a.Id,
                    Creator = new Board.Contracts.User.InfoUserResponse
                    {
                        UserName = a.Creator.UserName
                    },
                    Member = new Board.Contracts.User.InfoUserResponse
                    {
                        UserName = a.Member.UserName
                    },

                    Messages = a.Messages.Select(s => new InfoMessageResponse
                    {
                        Id = s.Id,
                        Containment = s.Containment
                    }).ToList()
                }).Take(take).Skip(skip).ToListAsync();
        }

        public async Task<InfoChatResponse> GetByIdAsync(Guid id,CancellationToken cancellation)
        {
            var existingchat = await _chatRepository.FindById(id,cancellation);
            return _mapper.Map<InfoChatResponse>(existingchat);
        }
    }
}
