using AutoMapper;
using Board.Contracts.Category;
using Board.Domain;
using Doska.AppServices.IRepository;
using Doska.AppServices.Services.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doska.AppServices.Services.Ad
{
    public class CategoryService : ICategoryService
    {
        public readonly ICategoryRepository _categoryRepository;
        public readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Guid> CreateCategoryAsync(string categoryname, CancellationToken cancellation)
        {
            var newCategory = new Category
            {
                Name = categoryname
            };
           
            await _categoryRepository.AddAsync(newCategory,cancellation);

            return newCategory.Id;
        }

        public async Task DeleteAsync(Guid id,CancellationToken cancellation)
        {
            var existingCategory = await _categoryRepository.FindById(id,cancellation);
            await _categoryRepository.DeleteAsync(existingCategory, cancellation);
        }

        public async Task<InfoCategoryResponse> EditCategoryAsync(Guid Id, string categoryname, CancellationToken cancellation)
        {
            var existingCategory = await _categoryRepository.FindById(Id,cancellation);
            existingCategory.Name = categoryname;
            await _categoryRepository.EditAdAsync(existingCategory,cancellation);

            return _mapper.Map<InfoCategoryResponse>(existingCategory);
        }

       

        public async Task<IReadOnlyCollection<InfoCategoryResponse>> GetAll(int take, int skip)
        {
            return await _categoryRepository.GetAll()
                .Select(a => new InfoCategoryResponse
                {
                    Id = a.Id,
                    Name = a.Name,
                    SubCategories = a.SubCategories.Select(s => new InfoCategoryResponse
                    {
                        Id = s.Id,
                        Name = s.Name
                    }).ToList()
                }).OrderBy(a => a.Id).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<InfoCategoryResponse> GetByIdAsync(Guid id, CancellationToken cancellation)
        {
            var existingCategory = await _categoryRepository.FindById(id,cancellation);
            return _mapper.Map<InfoCategoryResponse>(existingCategory);
        }

        
    }
}
