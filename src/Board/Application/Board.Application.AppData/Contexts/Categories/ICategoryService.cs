using Board.Contracts.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doska.AppServices.Services.Categories
{
    public interface ICategoryService
    {
        Task<InfoCategoryResponse> GetByIdAsync(Guid id, CancellationToken token);

        Task<Guid> CreateCategoryAsync(string categoryname, CancellationToken token);

        Task<IReadOnlyCollection<InfoCategoryResponse>> GetAll(int take, int skip);

        Task DeleteAsync(Guid id, CancellationToken token);

        Task<InfoCategoryResponse> EditCategoryAsync(Guid Id, string categoryname, CancellationToken token);
    }
}
