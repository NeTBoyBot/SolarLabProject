using Board.Contracts.Category;
using Doska.AppServices.Services.Ad;
using Doska.AppServices.Services.Categories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Doska.API.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Получение всех категорий
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        [HttpGet("/allCategories")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(int take, int skip)
        {
            var result = await _categoryService.GetAll(take, skip);

            return Ok(result);
        }

        /// <summary>
        /// Получение категории по Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpGet("/CategoryById")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCategoryById(Guid id,CancellationToken cancellation)
        {
            var result = await _categoryService.GetByIdAsync(id,cancellation);

            return Ok(result);
        }

        /// <summary>
        /// Создание категории
        /// </summary>
        /// <param name="categoryname"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpPost("/createCategory")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateCategory(string categoryname, CancellationToken cancellation)
        {
            var result = await _categoryService.CreateCategoryAsync(categoryname,cancellation);

            return Created("", result);
        }

        /// <summary>
        /// Обновление категории
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categoryname"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpPut("/updateCategory/{id}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateCategory(Guid id, string categoryname, CancellationToken cancellation)
        {
            var result = await _categoryService.EditCategoryAsync(id, categoryname,cancellation);

            return Ok(result);
        }

        /// <summary>
        /// Удаление категории
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpDelete("/deleteCategory/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteAd(Guid id, CancellationToken cancellation)
        {
            await _categoryService.DeleteAsync(id,cancellation);
            return Ok();
        }
    }
}
