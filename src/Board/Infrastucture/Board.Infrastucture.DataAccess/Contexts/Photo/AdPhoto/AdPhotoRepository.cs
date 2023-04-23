using Board.Application.AppData.Contexts.Photo.AdPhoto;
using Board.Infrastucture.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Infrastucture.DataAccess.Contexts.Photo.AdPhoto
{
    public class AdPhotoRepository : IAdPhotoRepository
    {
        public IRepository<Domain.Photos.AdPhoto> _baseRepository;

        public AdPhotoRepository(IRepository<Domain.Photos.AdPhoto> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public Task AddAsync(Domain.Photos.AdPhoto model, CancellationToken cancellation)
        {
            if(cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            return _baseRepository.AddAsync(model, cancellation);
        }

        public async Task DeleteAsync(Domain.Photos.AdPhoto model, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            await _baseRepository.DeleteAsync(model, cancellation);
        }

        public async Task EditFileAsync(Domain.Photos.AdPhoto edit, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            await _baseRepository.UpdateAsync(edit, cancellation);
        }

        public async Task<Domain.Photos.AdPhoto> FindById(Guid id, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            return await _baseRepository.GetByIdAsync(id, cancellation);
        }

        public IQueryable<Domain.Photos.AdPhoto> GetAll()
        {   
            return _baseRepository.GetAll();
        }
    }
}
