using Board.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Board.Application.AppData.Contexts.Ad
{
    public interface IAdRepository
    {

        Task<Domain.Ad> FindById(Guid id, CancellationToken cancellation);

        IQueryable<Domain.Ad> GetAll();

        public Task AddAsync(Domain.Ad model, CancellationToken cancellation);

        Task DeleteAsync(Domain.Ad model, CancellationToken cancellation);

        Task EditAdAsync(Domain.Ad edit, CancellationToken cancellation);
    }
}
