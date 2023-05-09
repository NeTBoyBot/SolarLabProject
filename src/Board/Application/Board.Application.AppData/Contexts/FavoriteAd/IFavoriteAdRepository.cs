using Board.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.FavoriteAd
{
    public interface IFavoriteAdRepository
    {
        Task<Domain.FavoriteAd> FindById(Guid id, CancellationToken cancellation);

        IQueryable<Domain.FavoriteAd> GetAll();

        public Task AddAsync(Domain.FavoriteAd model, CancellationToken cancellation);

        Task DeleteAsync(Domain.FavoriteAd model, CancellationToken cancellation);

        Task EditFavoriteAdAsync(Domain.FavoriteAd edit, CancellationToken cancellation);
    }
}
