using System;
using Board.Domain.Photos;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.Photo.AdPhoto
{
    public interface IAdPhotoRepository
    {
        Task<Domain.Photos.AdPhoto> FindById(Guid id, CancellationToken cancellation);

        IQueryable<Domain.Photos.AdPhoto> GetAll();

        public Task AddAsync(Domain.Photos.AdPhoto model, CancellationToken cancellation);

        Task DeleteAsync(Domain.Photos.AdPhoto model, CancellationToken cancellation);

        Task EditFileAsync(Domain.Photos.AdPhoto edit, CancellationToken cancellation);
    }
}
