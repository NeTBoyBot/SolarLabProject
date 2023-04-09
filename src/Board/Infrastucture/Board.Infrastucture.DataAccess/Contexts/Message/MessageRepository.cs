using Doska.AppServices.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Board.Domain;
using Board.Infrastucture.Repository;

namespace Doska.DataAccess.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        public readonly IRepository<Message> _baseRepository;

        public MessageRepository(IRepository<Message> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public Task AddAsync(Message model,CancellationToken cancellation)
        {
            return _baseRepository.AddAsync(model,cancellation);
        }

        public async Task DeleteAsync(Message model, CancellationToken cancellation)
        {
            await _baseRepository.DeleteAsync(model,cancellation);
        }

        public async Task EditAdAsync(Message edit, CancellationToken cancellation)
        {
            await _baseRepository.UpdateAsync(edit,cancellation);
        }

        public async Task<Message> FindById(Guid id, CancellationToken cancellation)
        {
            return await _baseRepository.GetByIdAsync(id,cancellation);
        }

        public IQueryable<Message> GetAll()
        {
            return _baseRepository.GetAll();
        }
    }
}
