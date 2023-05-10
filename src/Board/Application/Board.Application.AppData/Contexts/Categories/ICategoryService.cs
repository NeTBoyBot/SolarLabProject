using Board.Contracts.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doska.AppServices.Services.Categories
{
    public interface ICategoryService
    {
        /// <summary>
        /// Получение категории по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<InfoCategoryResponse> GetByIdAsync(Guid id, CancellationToken token);

        /// <summary>
        /// Создание категории
        /// </summary>
        /// <param name="categoryname"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<Guid> CreateCategoryAsync(CreateCategoryRequest request, CancellationToken token);

        /// <summary>
        /// Получение всех категорий
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        Task<IReadOnlyCollection<InfoCategoryResponse>> GetAll(int take, int skip);

        /// <summary>
        /// Удаление категории
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id, CancellationToken token);

        /// <summary>
        /// Изменение категории
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="categoryname"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<InfoCategoryResponse> EditCategoryAsync(Guid Id, string categoryname, CancellationToken token);
    }
}
