using Board.Contracts.Photo.AdPhoto;
using Board.Contracts.TelegramClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.TelegramClient
{
    public interface ITelegramClientService
    {
        /// <summary>
        /// Получение телеграм клиента по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<InfoTelegramClientResponse> GetTelegramClientByIdAsync(Guid id, CancellationToken cancellation);

        /// <summary>
        /// Создание телеграм клиента
        /// </summary>
        /// <param name="createAd"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<long> CreateTelegramClientAsync(CreateTelegramClientRequest request, CancellationToken cancellation);

        /// <summary>
        /// Получение всех телеграм клиентов
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        Task<IReadOnlyCollection<InfoTelegramClientResponse>> GetAllTelegramClients(int take, int skip);

        /// <summary>
        /// Удаление телеграм клиента
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task DeleteTelegramClientPhotoAsync(Guid id, CancellationToken cancellation);
    }
}
