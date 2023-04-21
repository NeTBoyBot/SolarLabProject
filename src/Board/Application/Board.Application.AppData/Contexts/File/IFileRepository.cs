using Board.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.File
{
    public interface IFileRepository
    {
        Task<Domain.File> FindById(Guid id, CancellationToken cancellation);

        IQueryable<Domain.File> GetAll();

        public Task AddAsync(Domain.File model, CancellationToken cancellation);

        Task DeleteAsync(Domain.File model, CancellationToken cancellation);

        Task EditFileAsync(Domain.File edit, CancellationToken cancellation);
    }
}
