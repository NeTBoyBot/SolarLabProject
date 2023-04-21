using Board.Contracts.Ad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doska.AppServices.Services.Ad
{
    public interface IAdService
    {
        /// <summary>
        /// Получение объявления по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<InfoAdResponse> GetByIdAsync(Guid id, CancellationToken token);

        /// <summary>
        /// Создание объявление
        /// </summary>
        /// <param name="ownerId"></param>
        /// <param name="createAd"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<Guid> CreateAdAsync(Guid ownerId,CreateAdRequest createAd, CancellationToken token);

        /// <summary>
        /// Получение всех объявлений
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        Task<IReadOnlyCollection<InfoAdResponse>> GetAll(int take, int skip);

        /// <summary>
        /// Удаление объявления
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id, CancellationToken token);

        /// <summary>
        /// Изменение объявления
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="editAd"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<InfoAdResponse> EditAdAsync(Guid Id,CreateAdRequest editAd, CancellationToken token);

        /// <summary>
        /// Получение объявления по фильтру
        /// </summary>
        /// <param name="name"></param>
        /// <param name="subcategoryId"></param>
        /// <returns></returns>
        Task<IReadOnlyCollection<InfoAdResponse>> GetAdFiltered(string? name, Guid? subcategoryId);

        /// <summary>
        /// Получение всех объявлений авторизованного пользователя
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IReadOnlyCollection<InfoAdResponse>> GetAllUserAds(int take, int skip,CancellationToken token);
    }
}
