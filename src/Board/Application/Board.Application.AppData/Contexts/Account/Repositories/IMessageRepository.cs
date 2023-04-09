using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Board.Domain;

namespace Doska.AppServices.IRepository
{
    public interface IMessageRepository
    {
        Task<Message> FindById(Guid id, CancellationToken cancellation);

        IQueryable<Message> GetAll();

        public Task AddAsync(Message model, CancellationToken cancellation);

        Task DeleteAsync(Message model, CancellationToken cancellation);
    }
}
