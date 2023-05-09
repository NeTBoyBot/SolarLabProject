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
        /// <summary>
        /// Получение комментария по Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<InfoCommentResponse> GetByIdAsync(Guid id, CancellationToken cancellation);

        /// <summary>
        /// Получение всех комменатриев авторизованного пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<ICollection<InfoCommentResponse>> GetAllCommentsForUser(Guid userId, CancellationToken cancellation);

        /// <summary>
        /// Создание комментария
        /// </summary>
        /// <param name="createAd"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<Guid> CreateCommentAsync(CreateCommentRequest createAd, CancellationToken cancellation);

        /// <summary>
        /// Получение всех комментариев
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        Task<IReadOnlyCollection<InfoCommentResponse>> GetAll(int take, int skip);

        /// <summary>
        /// Удаление комментария
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id, CancellationToken cancellation);
    }
}
