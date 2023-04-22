using AutoMapper;
using Board.Application.AppData.Contexts.Categories;
using Board.Contracts.Category;
using Board.Domain;
using Doska.AppServices.Services.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Doska.AppServices.Services.Ad
{
    public class CategoryService : ICategoryService
    {
        public readonly ICategoryRepository _categoryRepository;
        public readonly IMapper _mapper;
        public readonly ILogger<CategoryService> _logger;
        public readonly IMemoryCache _cache;

        private const string CategoriesCachingKey = "Categories";

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, ILogger<CategoryService> logger,
            IMemoryCache cache)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _logger = logger;
            _cache = cache;
        }

        public async Task<Guid> CreateCategoryAsync(string categoryname, CancellationToken cancellation)
        {

            _logger.LogInformation($"Создание категории под названием: {categoryname}");

            var newCategory = new Category
            {
                Name = categoryname
            };
           
            await _categoryRepository.AddAsync(newCategory,cancellation);

            return newCategory.Id;
        }

        public async Task DeleteAsync(Guid id,CancellationToken cancellation)
        {
            _logger.LogInformation($"Удаление категории под Id: {id}");

            var existingCategory = await _categoryRepository.FindById(id,cancellation);
            await _categoryRepository.DeleteAsync(existingCategory, cancellation);
        }

        public async Task<InfoCategoryResponse> EditCategoryAsync(Guid Id, string categoryname, CancellationToken cancellation)
        {
            _logger.LogInformation($"Изменение имени категории под id {Id} на имя {categoryname}");

            var existingCategory = await _categoryRepository.FindById(Id,cancellation);
            existingCategory.Name = categoryname;
            await _categoryRepository.EditAdAsync(existingCategory,cancellation);

            return _mapper.Map<InfoCategoryResponse>(existingCategory);
        }

       

        public async Task<IReadOnlyCollection<InfoCategoryResponse>> GetAll(int take, int skip)
        {
            _logger.LogInformation($"Получение всех категорий");

            if (_cache.TryGetValue(CategoriesCachingKey, out IReadOnlyCollection<InfoCategoryResponse> result))
            {
                _logger.LogInformation("Данные получены из кеша");
                return result;
            }
            else
            {
                _cache.Set(CategoriesCachingKey,
                    await _categoryRepository.GetAll()
                .Select(a => new InfoCategoryResponse
                {
                    Id = a.Id,
                    Name = a.Name
                }).OrderBy(a => a.Id).Skip(skip).Take(take).ToListAsync(),
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
            }

            return await _categoryRepository.GetAll()
                .Select(a => new InfoCategoryResponse
                {
                    Id = a.Id,
                    Name = a.Name
                }).OrderBy(a => a.Id).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<InfoCategoryResponse> GetByIdAsync(Guid id, CancellationToken cancellation)
        {
            _logger.LogInformation($"Поиск объявления под id: {id}");

            var existingCategory = await _categoryRepository.FindById(id,cancellation);
            return _mapper.Map<InfoCategoryResponse>(existingCategory);
        }

        
    }
}
