using Board.Contracts.FavoriteAd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doska.AppServices.Services.FavoriteAd
{
    public interface IFavoriteAdService
    {
        /// <summary>
        /// Получение избранного объявления по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<InfoFavoriteAdResponse> GetByIdAsync(Guid id, CancellationToken cancellation);

        /// <summary>
        /// Добавление избранного объявления
        /// </summary>
        /// <param name="createAd"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<Guid> CreateFavoriteAdAsync(CreateFavoriteAdRequest createAd, CancellationToken cancellation);

        /// <summary>
        /// Получение всех избранных объявлений
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        Task<IReadOnlyCollection<InfoFavoriteAdResponse>> GetAll(int take, int skip);

        /// <summary>
        /// Удаление объявления из избранных
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id, CancellationToken cancellation);

        /// <summary>
        /// Получение всех избранных объявлений авторизованного пользователя
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IReadOnlyCollection<InfoFavoriteAdResponse>> GetAllUserFavorites(int take, int skip, CancellationToken token);
    }
}
