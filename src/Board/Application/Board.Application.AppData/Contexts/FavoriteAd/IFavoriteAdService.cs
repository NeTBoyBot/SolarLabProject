using Board.Contracts.FavoriteAd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doska.AppServices.Services.FavoriteAd
{
    public interface IFavoriteAdService
    {
        Task<InfoFavoriteAdResponse> GetByIdAsync(Guid id, CancellationToken cancellation);

        Task<Guid> CreateFavoriteAdAsync(CreateFavoriteAdRequest createAd, CancellationToken cancellation);

        Task<IReadOnlyCollection<InfoFavoriteAdResponse>> GetAll(int take, int skip);

        Task DeleteAsync(Guid id, CancellationToken cancellation);

        Task<IReadOnlyCollection<InfoFavoriteAdResponse>> GetAllUserFavorites(int take, int skip, CancellationToken token);
    }
}
