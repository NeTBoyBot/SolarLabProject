using Board.Contracts.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doska.AppServices.Services.Comment
{
    public interface ICommentService
    {
        Task<InfoCommentResponse> GetByIdAsync(Guid id, CancellationToken cancellation);

        Task<ICollection<InfoCommentResponse>> GetAllCommentsForUser(Guid userId, CancellationToken cancellation);

        Task<Guid> CreateCommentAsync(CreateCommentRequest createAd, CancellationToken cancellation);

        Task<IReadOnlyCollection<InfoCommentResponse>> GetAll(int take, int skip);

        Task DeleteAsync(Guid id, CancellationToken cancellation);
    }
}
