
using Board.Contracts.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doska.AppServices.Services.Chat
{
    public interface IChatService
    {
        Task<InfoChatResponse> GetByIdAsync(Guid id, CancellationToken cancellation);

        Task<Guid> CreateChatAsync(CreateChatRequest createAd, CancellationToken cancellation);

        Task<IReadOnlyCollection<InfoChatResponse>> GetAll(int take, int skip);

        Task DeleteAsync(Guid id, CancellationToken cancellation);

        Task<IReadOnlyCollection<InfoChatResponse>> GetAllChatsForUser(Guid UserId, CancellationToken cancellation);

        Task<IReadOnlyCollection<InfoChatResponse>> GetAllUserChats(int take, int skip, CancellationToken token);
    }
}
