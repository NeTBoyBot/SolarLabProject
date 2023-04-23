using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.Photo.UserPhoto
{
    public interface IUserPhotoRepository
    {
        Task<Domain.Photos.UserPhoto> FindById(Guid id, CancellationToken cancellation);

        IQueryable<Domain.Photos.UserPhoto> GetAll();

        public Task AddAsync(Domain.Photos.UserPhoto model, CancellationToken cancellation);

        Task DeleteAsync(Domain.Photos.UserPhoto model, CancellationToken cancellation);

        Task EditFileAsync(Domain.Photos.UserPhoto edit, CancellationToken cancellation);
    }
}
