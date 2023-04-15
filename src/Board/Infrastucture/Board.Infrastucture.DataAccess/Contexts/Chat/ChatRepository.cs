using Board.Application.AppData.Contexts.Chat;
using Board.Domain;
using Board.Infrastucture.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doska.DataAccess.Repositories
{
    public class ChatRepository : IChatRepository
    {
        public readonly IRepository<Chat> _baseRepository;

        public ChatRepository(IRepository<Chat> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public Task AddAsync(Chat model, CancellationToken cancellationl)
        {
            return _baseRepository.AddAsync(model,cancellationl);
        }

        public async Task DeleteAsync(Chat model, CancellationToken cancellation)
        {
            await _baseRepository.DeleteAsync(model,cancellation);
        }

        public async Task EditAdAsync(Chat edit, CancellationToken cancellation)
        {
            await _baseRepository.UpdateAsync(edit,cancellation);
        }

        public async Task<Chat> FindById(Guid id, CancellationToken cancellation)
        {
            return await _baseRepository.GetByIdAsync(id,cancellation);
        }

        public IQueryable<Chat> GetAll()
        {
            return _baseRepository.GetAll();
        }
    }
}
