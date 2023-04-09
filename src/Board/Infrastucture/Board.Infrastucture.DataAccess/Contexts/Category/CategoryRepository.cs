using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Board.Domain;
using Board.Infrastucture.Repository;
using Doska.AppServices.IRepository;

namespace Doska.DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public readonly IRepository<Category> _baseRepository;

        public CategoryRepository(IRepository<Category> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public Task AddAsync(Category model, CancellationToken cancellation)
        {
            return _baseRepository.AddAsync(model,cancellation);
        }

        public async Task DeleteAsync(Category model, CancellationToken cancellation)
        {
            await _baseRepository.DeleteAsync(model,cancellation);
        }

        public async Task EditAdAsync(Category edit, CancellationToken cancellation)
        {
            await _baseRepository.UpdateAsync(edit,cancellation);
        }

        public async Task<Category> FindById(Guid id, CancellationToken cancellation)
        {
            return await _baseRepository.GetByIdAsync(id,cancellation);
        }

        public IQueryable<Category> GetAll()
        {
            return _baseRepository.GetAll();
        }
    }
}
