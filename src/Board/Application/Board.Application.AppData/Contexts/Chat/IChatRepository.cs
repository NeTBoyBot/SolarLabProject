using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Board.Domain;

namespace Board.Application.AppData.Contexts.Chat
{
    public interface IChatRepository
    {
        Task<Domain.Chat> FindById(Guid id, CancellationToken cancellation);

        IQueryable<Domain.Chat> GetAll();

        public Task AddAsync(Domain.Chat model, CancellationToken cancellation);

        Task DeleteAsync(Domain.Chat model, CancellationToken cancellation);
    }
}
