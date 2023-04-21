using Board.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.Categories
{
    public interface ICategoryRepository
    {
        Task<Category> FindById(Guid id, CancellationToken cancellation);

        IQueryable<Category> GetAll();

        public Task AddAsync(Category model, CancellationToken cancellation);

        Task DeleteAsync(Category model, CancellationToken cancellation);

        Task EditAdAsync(Category edit, CancellationToken cancellation);
    }
}
