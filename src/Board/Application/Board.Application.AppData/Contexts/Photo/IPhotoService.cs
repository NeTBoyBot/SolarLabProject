using Board.Contracts.File;
using Board.Contracts.Photo.AdPhoto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.Photo
{
    public interface IPhotoService
    {
        /// <summary>
        /// Получение фото по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<InfoAdPhotoResponse> GetAdPhotoByIdAsync(Guid id, CancellationToken cancellation);

        /// <summary>
        /// Получение фото по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<InfoUserPhotoResponse> GetUserPhotoByIdAsync(Guid id, CancellationToken cancellation);

        /// <summary>
        /// Создание фото пользователя
        /// </summary>
        /// <param name="createAd"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<Guid> CreateUserPhotoAsync(Guid UserId, byte[] bytes, CancellationToken cancellation);

        /// <summary>
        /// Создание фото объявления
        /// </summary>
        /// <param name="createAd"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<Guid> CreateAdPhotoAsync(Guid AdId, byte[] bytes, CancellationToken cancellation);

        /// <summary>
        /// Получение всех фото объявлений
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        Task<IReadOnlyCollection<InfoAdPhotoResponse>> GetAllAdPhotos(int take, int skip);


        /// <summary>
        /// Получение всех фото пользователей
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        Task<IReadOnlyCollection<InfoUserPhotoResponse>> GetAllUserPhotos(int take, int skip);

        /// <summary>
        /// Удаление фото пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task DeleteUserPhotoAsync(Guid id, CancellationToken cancellation);

        /// <summary>
        /// Удаление фото объявления
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task DeleteAdPhotoAsync(Guid id, CancellationToken cancellation);

        /// <summary>
        /// Получение содержимого фотографии объявления
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<string> GetAdPhotoContent(Guid id, CancellationToken cancellation);

        /// <summary>
        /// Получение содержимого фотографии пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<string> GetUserPhotoContent(Guid id, CancellationToken cancellation);



    }
}
