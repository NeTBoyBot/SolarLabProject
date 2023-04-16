using Board.Contracts.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doska.AppServices.Services.Message
{
    public interface IMessageService
    {
        Task<InfoMessageResponse> GetByIdAsync(Guid id, CancellationToken cancellation);

        Task<Guid> CreateMessageAsync(Guid senderId,CreateMessageRequest createAd, CancellationToken cancellation);

        Task<IReadOnlyCollection<InfoMessageResponse>> GetAll(int take, int skip);

        Task DeleteAsync(Guid id, CancellationToken cancellation);

        Task<IReadOnlyCollection<string>> GetAllInChat(Guid ChatId, CancellationToken cancellation);
    }
}
