using Board.Application.AppData.Contexts.TelegramClient;
using Board.Infrastucture.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Infrastucture.DataAccess.Contexts.TelegramClient
{
    public class TelegramClientRepository : ITelegramClientRepository
    {
        public IRepository<Domain.TelegramClient> _baseRepository;

        public TelegramClientRepository(IRepository<Domain.TelegramClient> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public Task AddAsync(Domain.TelegramClient model, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            return _baseRepository.AddAsync(model, cancellation);
        }

        public async Task DeleteAsync(Domain.TelegramClient model, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            await _baseRepository.DeleteAsync(model, cancellation);
        }

        public async Task EditClientAsync(Domain.TelegramClient edit, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            await _baseRepository.UpdateAsync(edit, cancellation);
        }

        public async Task<Domain.TelegramClient> FindById(Guid id, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            return await _baseRepository.GetByIdAsync(id, cancellation);
        }

        public IQueryable<Domain.TelegramClient> GetAll()
        {
            return _baseRepository.GetAll();
        }
    }
}
