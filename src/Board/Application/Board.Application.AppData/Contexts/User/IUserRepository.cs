using Board.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.User
{
    public interface IUserRepository
    {
        Task<Domain.User> FindWhere(Expression<Func<Domain.User, bool>> predicate, CancellationToken token);

        Task<Domain.User> FindById(Guid id, CancellationToken cancellation);

        IQueryable<Domain.User> GetAll();

        public Task AddAsync(Domain.User model, CancellationToken cancellation);

        Task DeleteAsync(Domain.User model, CancellationToken cancellation);

        Task EditUserAsync(Domain.User edit, CancellationToken cancellation);

    }
}
