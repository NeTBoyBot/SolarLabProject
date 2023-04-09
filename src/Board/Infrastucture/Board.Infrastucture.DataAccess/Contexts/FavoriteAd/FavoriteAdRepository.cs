using Board.Domain;
using Board.Infrastucture.Repository;
using Doska.AppServices.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doska.DataAccess.Repositories
{
    public class FavoriteAdRepository : IFavoriteAdRepository
    {
        public readonly IRepository<FavoriteAd> _baseRepository;

        public FavoriteAdRepository(IRepository<FavoriteAd> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public Task AddAsync(FavoriteAd model, CancellationToken cancellation)
        {
            return _baseRepository.AddAsync(model,cancellation);
        }

        public async Task DeleteAsync(FavoriteAd model, CancellationToken cancellation)
        {
            await _baseRepository.DeleteAsync(model,cancellation);
        }

        public async Task EditFavoriteAdAsync(FavoriteAd edit, CancellationToken cancellation)
        {
            await _baseRepository.UpdateAsync(edit,cancellation);
        }

        public async Task<FavoriteAd> FindById(Guid id, CancellationToken cancellation)
        {
            return await _baseRepository.GetByIdAsync(id,cancellation);
        }

        public IQueryable<FavoriteAd> GetAll()
        {
            return _baseRepository.GetAll();
        }
    }
}
