
using Board.Application.AppData.Contexts.Comment;
using Board.Domain;
using Board.Infrastucture.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doska.DataAccess.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        public readonly IRepository<Comment> _baseRepository;

        public CommentRepository(IRepository<Comment> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public Task AddAsync(Comment model, CancellationToken cancellation)
        {
            return _baseRepository.AddAsync(model,cancellation);
        }

        public async Task DeleteAsync(Comment model, CancellationToken cancellation)
        {
            await _baseRepository.DeleteAsync(model,cancellation);
        }

        public async Task EditCommentAsync(Comment edit, CancellationToken cancellation)
        {
            await _baseRepository.UpdateAsync(edit,cancellation);
        }

        public async Task<Comment> FindById(Guid id, CancellationToken cancellation)
        {
            return await _baseRepository.GetByIdAsync(id,cancellation);
        }

        public IQueryable<Comment> GetAll()
        {
            return _baseRepository.GetAll();
        }
    }
}
