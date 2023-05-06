using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.TelegramClient
{
    public interface ITelegramClientRepository
    {
        Task<Domain.TelegramClient> FindById(Guid id, CancellationToken cancellation);

        IQueryable<Domain.TelegramClient> GetAll();

        public Task AddAsync(Domain.TelegramClient model, CancellationToken cancellation);

        Task DeleteAsync(Domain.TelegramClient model, CancellationToken cancellation);

        Task EditClientAsync(Domain.TelegramClient edit, CancellationToken cancellation);
    }
}
