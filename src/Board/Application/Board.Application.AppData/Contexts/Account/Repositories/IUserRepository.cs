using Board.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Doska.AppServices.IRepository
{
    public interface IUserRepository
    {
        Task<User> FindWhere(Expression<Func<User, bool>> predicate, CancellationToken token);

        Task<User> FindById(Guid id, CancellationToken cancellation);

        IQueryable<User> GetAll();

        public Task AddAsync(User model, CancellationToken cancellation);

        Task DeleteAsync(User model, CancellationToken cancellation);

        Task EditUserAsync(User edit, CancellationToken cancellation);

    }
}
