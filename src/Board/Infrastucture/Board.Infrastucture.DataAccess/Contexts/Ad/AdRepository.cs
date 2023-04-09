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
    public class AdRepository : IAdRepository
    {
        public readonly IRepository<Ad> _baseRepository;

        public AdRepository(IRepository<Ad> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public Task AddAsync(Ad model, CancellationToken cancellation)
        {
            return _baseRepository.AddAsync(model,cancellation);
        }

        public async Task DeleteAsync(Ad model, CancellationToken cancellation)
        {
            await _baseRepository.DeleteAsync(model,cancellation);
        }

        public async Task EditAdAsync(Ad edit, CancellationToken cancellation)
        {
            await _baseRepository.UpdateAsync(edit,cancellation);
        }

        public async Task<Ad> FindById(Guid id, CancellationToken cancellation)
        {
            return await _baseRepository.GetByIdAsync(id,cancellation);
        }

        public IQueryable<Ad> GetAll()
        {
            return _baseRepository.GetAll();
        }
    }
}
