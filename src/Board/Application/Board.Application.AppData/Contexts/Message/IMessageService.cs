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
        /// <summary>
        /// Получение сообщения по Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<InfoMessageResponse> GetByIdAsync(Guid id, CancellationToken cancellation);

        /// <summary>
        /// Создание сообщения 
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="createAd"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<Guid> CreateMessageAsync(Guid senderId,CreateMessageRequest createAd, CancellationToken cancellation);

        /// <summary>
        /// Получение всех сообщений
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        Task<IReadOnlyCollection<InfoMessageResponse>> GetAll(int take, int skip);

        /// <summary>
        /// Удаление сообщения
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id, CancellationToken cancellation);
        
        /// <summary>
        /// Получение всех сообщений в чате
        /// </summary>
        /// <param name="ChatId"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<IReadOnlyCollection<string>> GetAllInChat(Guid ChatId, CancellationToken cancellation);
    }
}
