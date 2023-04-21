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
        /// <summary>
        /// Получение файла по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<InfoFileResponse> GetByIdAsync(Guid id, CancellationToken cancellation);

        /// <summary>
        /// Создание файла
        /// </summary>
        /// <param name="createAd"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<Guid> CreateFileAsync(CreateFileRequest createAd, CancellationToken cancellation);

        /// <summary>
        /// Получение всех файлов
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        Task<IReadOnlyCollection<InfoFileResponse>> GetAll(int take, int skip);

        /// <summary>
        /// Удаление файла
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id, CancellationToken cancellation);
    }
}
