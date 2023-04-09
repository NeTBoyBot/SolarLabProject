using Board.Contracts.Ad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doska.AppServices.Services.Ad
{
    public interface IAdService
    {
        Task<InfoAdResponse> GetByIdAsync(Guid id, CancellationToken token);

        Task<Guid> CreateAdAsync(CreateAdRequest createAd, CancellationToken token);

        Task<IReadOnlyCollection<InfoAdResponse>> GetAll(int take, int skip);

        Task DeleteAsync(Guid id, CancellationToken token);

        Task<InfoAdResponse> EditAdAsync(Guid Id,CreateAdRequest editAd, CancellationToken token);

        Task<IReadOnlyCollection<InfoAdResponse>> GetAdFiltered(string? name, Guid? subcategoryId);

        Task<IReadOnlyCollection<InfoAdResponse>> GetAllUserAds(int take, int skip,CancellationToken token);
    }
}
