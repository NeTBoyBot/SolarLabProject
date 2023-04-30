using Board.Contracts.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.Role
{
    public interface IRoleService
    {
        /// <summary>
        /// Получение роли по Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<InfoRoleResponse> GetByIdAsync(Guid id, CancellationToken cancellation);

        /// <summary>
        /// Создание роли
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="createAd"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<Guid> CreateRoleAsync(string name, CancellationToken cancellation);

        /// <summary>
        /// Получение всех ролей
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        Task<IReadOnlyCollection<InfoRoleResponse>> GetAll(int take, int skip);

        /// <summary>
        /// Удаление роли
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id, CancellationToken cancellation);
    }
}
