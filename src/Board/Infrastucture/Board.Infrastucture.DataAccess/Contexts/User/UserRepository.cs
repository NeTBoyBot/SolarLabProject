using Board.Application.AppData.Contexts.User;
using Board.Domain;
using Board.Infrastucture.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Doska.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        public readonly IRepository<User> _baseRepository;

        public UserRepository(IRepository<User> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public Task AddAsync(User model,CancellationToken cancellation)
        {
            return _baseRepository.AddAsync(model, cancellation);
        }

        public async Task DeleteAsync(User model, CancellationToken cancellation)
        {
            await _baseRepository.DeleteAsync(model, cancellation);
        }

        public async Task EditUserAsync(User edit, CancellationToken cancellation)
        {
            await _baseRepository.UpdateAsync(edit,cancellation);
        }

        public async Task<User> FindById(Guid id, CancellationToken cancellation)
        {
            return await _baseRepository.GetAll().Where(i => i.Id == id).Include(i => i.Photos).FirstOrDefaultAsync();
        }

        public async Task<User> FindWhere(Expression<Func<User, bool>> predicate, CancellationToken token)
        {
            var data = _baseRepository.GetAllFiltered(predicate);

            return await data.Where(predicate).FirstOrDefaultAsync(token);
        }

        public IQueryable<User> GetAll()
        {
            return _baseRepository.GetAll();
        }
    }
}
