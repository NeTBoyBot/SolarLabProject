using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Board.Domain;

namespace Doska.AppServices.IRepository
{
    public interface IChatRepository
    {
        Task<Chat> FindById(Guid id, CancellationToken cancellation);

        IQueryable<Chat> GetAll();

        public Task AddAsync(Chat model, CancellationToken cancellation);

        Task DeleteAsync(Chat model, CancellationToken cancellation);
    }
}
