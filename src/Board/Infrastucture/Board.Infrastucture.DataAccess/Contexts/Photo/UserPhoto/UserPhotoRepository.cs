using Board.Application.AppData.Contexts.Photo.UserPhoto;
using Board.Infrastucture.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Infrastucture.DataAccess.Contexts.Photo.UserPhoto
{
    public class UserPhotoRepository : IUserPhotoRepository
    {
        public IRepository<Domain.Photos.UserPhoto> _baseRepository;

        public UserPhotoRepository(IRepository<Domain.Photos.UserPhoto> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public Task AddAsync(Domain.Photos.UserPhoto model, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            return _baseRepository.AddAsync(model, cancellation);
        }

        public async Task DeleteAsync(Domain.Photos.UserPhoto model, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            await _baseRepository.DeleteAsync(model, cancellation);
        }

        public async Task EditFileAsync(Domain.Photos.UserPhoto edit, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            await _baseRepository.UpdateAsync(edit, cancellation);
        }

        public async Task<Domain.Photos.UserPhoto> FindById(Guid id, CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            return await _baseRepository.GetByIdAsync(id, cancellation);
        }

        public IQueryable<Domain.Photos.UserPhoto> GetAll()
        {
            return _baseRepository.GetAll();
        }
    }
}
