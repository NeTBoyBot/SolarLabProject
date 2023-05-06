using AutoMapper;
using Board.Contracts.TelegramClient;
using Board.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.TelegramClient
{
    public class TelegramClientService : ITelegramClientService
    {
        private readonly ITelegramClientRepository _repository;
        private readonly IMapper _mapper;

        public TelegramClientService(ITelegramClientRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<long> CreateTelegramClientAsync(CreateTelegramClientRequest request, CancellationToken cancellation)
        {
            var client = _mapper.Map<Domain.TelegramClient>(request);

            await _repository.AddAsync(client,cancellation);

            return client.Id;
        }

        public async Task DeleteTelegramClientPhotoAsync(Guid id, CancellationToken cancellation)
        {
            var user = await _repository.FindById(id, cancellation);
            if (user == null)
                throw new Exception("Данный пользователь не найден!");

            await _repository.DeleteAsync(user, cancellation);
        }

        public async Task<IReadOnlyCollection<InfoTelegramClientResponse>> GetAllTelegramClients(int take, int skip)
        {
            var clients = await _repository.GetAll().Select(c => new InfoTelegramClientResponse
            {
                ChatId = c.ChatId,
                Id = c.Id,
                UserId = c.UserId
            }).Take(take).Skip(skip).ToListAsync();

            return clients;
        }

        public async Task<InfoTelegramClientResponse> GetTelegramClientByIdAsync(Guid id, CancellationToken cancellation)
        {
            var user = await _repository.FindById(id, cancellation);
            if (user == null)
                throw new Exception("Данный пользователь не найден!");

            return _mapper.Map<InfoTelegramClientResponse>(user);
        }
    }
}
