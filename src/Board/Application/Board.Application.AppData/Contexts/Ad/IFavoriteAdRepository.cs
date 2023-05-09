using Board.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.Ad
{
    public interface IFavoriteAdRepository
    {
        Task<FavoriteAd> FindById(Guid id, CancellationToken cancellation);

        IQueryable<FavoriteAd> GetAll();

        public Task AddAsync(FavoriteAd model, CancellationToken cancellation);

        Task DeleteAsync(FavoriteAd model, CancellationToken cancellation);

        Task EditFavoriteAdAsync(FavoriteAd edit, CancellationToken cancellation);
    }
}
