using Board.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.Comment
{
    public interface ICommentRepository
    {
        Task<Domain.Comment> FindById(Guid id, CancellationToken cancellation);

        IQueryable<Domain.Comment> GetAll();

        public Task AddAsync(Domain.Comment model, CancellationToken cancellation);

        Task DeleteAsync(Domain.Comment model, CancellationToken cancellation);
    }
}
