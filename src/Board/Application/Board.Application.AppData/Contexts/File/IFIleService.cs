using Board.Contracts.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.File
{
    public interface IFileService
    {
        Task<InfoFileResponse> GetByIdAsync(Guid id, CancellationToken cancellation);

        Task<Guid> CreateFileAsync(CreateFileRequest createAd, CancellationToken cancellation);

        Task<IReadOnlyCollection<InfoFileResponse>> GetAll(int take, int skip);

        Task DeleteAsync(Guid id, CancellationToken cancellation);
    }
}
