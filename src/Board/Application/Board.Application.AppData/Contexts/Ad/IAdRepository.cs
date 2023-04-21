
using Board.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doska.AppServices.IRepository
{
    public interface IAdRepository
    {

        Task<Ad> FindById(Guid id, CancellationToken cancellation);

        IQueryable<Ad> GetAll();

        public Task AddAsync(Ad model, CancellationToken cancellation);

        Task DeleteAsync(Ad model, CancellationToken cancellation);

        Task EditAdAsync(Ad edit, CancellationToken cancellation);
    }
}
