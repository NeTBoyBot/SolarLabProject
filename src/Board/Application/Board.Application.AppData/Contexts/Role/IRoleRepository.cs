using Board.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.Role
{
    public interface IRoleRepository
    {
        Task<Domain.Role> FindById(Guid id, CancellationToken cancellation);

        IQueryable<Domain.Role> GetAll();

        public Task AddAsync(Domain.Role model, CancellationToken cancellation);

        Task DeleteAsync(Domain.Role model, CancellationToken cancellation);

        Task EditRoleAsync(Domain.Role edit, CancellationToken cancellation);
    }
}
