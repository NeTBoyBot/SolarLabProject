using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Board.Domain;

namespace Board.Application.AppData.Contexts.Message
{
    public interface IMessageRepository
    {
        Task<Domain.Message> FindById(Guid id, CancellationToken cancellation);

        IQueryable<Domain.Message> GetAll();

        public Task AddAsync(Domain.Message model, CancellationToken cancellation);

        Task DeleteAsync(Domain.Message model, CancellationToken cancellation);
    }
}
