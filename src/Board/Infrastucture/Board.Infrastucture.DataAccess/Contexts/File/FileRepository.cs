using Board.Application.AppData.Contexts.File;
using Board.Infrastucture.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Infrastucture.DataAccess.Contexts.File
{
    public class FileRepository : IFileRepository
    {
        public IRepository<Domain.File> _baseRepository;

        public FileRepository(IRepository<Domain.File> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public Task AddAsync(Domain.File model, CancellationToken cancellation)
        {
            return _baseRepository.AddAsync(model, cancellation);
        }

        public async Task DeleteAsync(Domain.File model, CancellationToken cancellation)
        {
            await _baseRepository.DeleteAsync(model, cancellation);
        }

        public async Task EditFileAsync(Domain.File edit, CancellationToken cancellation)
        {
            await _baseRepository.UpdateAsync(edit, cancellation);
        }

        public async Task<Domain.File> FindById(Guid id, CancellationToken cancellation)
        {
            return await _baseRepository.GetByIdAsync(id, cancellation);
        }

        public IQueryable<Domain.File> GetAll()
        {
            return _baseRepository.GetAll();
        }
    }
}
