using Board.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doska.AppServices.IRepository
{
    public interface ICommentRepository
    {
        Task<Comment> FindById(Guid id, CancellationToken cancellation);

        IQueryable<Comment> GetAll();

        public Task AddAsync(Comment model, CancellationToken cancellation);

        Task DeleteAsync(Comment model, CancellationToken cancellation);
    }
}
