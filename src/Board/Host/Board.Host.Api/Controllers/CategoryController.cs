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
        [HttpGet("/allCategories")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(int take, int skip)
        {
            var result = await _categoryService.GetAll(take, skip);

            return Ok(result);
        }

        //[HttpGet("/CategorieById")]
        //[ProducesResponseType(typeof(IReadOnlyCollection<InfoCategoryResponse>), (int)HttpStatusCode.OK)]
        //public async Task<IActionResult> GetCategoryById(Guid id)
        //{
        //    var result = await _categoryService.GetByIdAsync(id);

        //    return Ok(result);
        //}

        [HttpPost("/createCategory")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateAd(string categoryname, CancellationToken cancellation)
        {
            var result = await _categoryService.CreateCategoryAsync(categoryname,cancellation);

            return Created("", result);
        }

        [HttpPut("/updateCategory/{id}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateAd(Guid id, string categoryname, CancellationToken cancellation)
        {
            var result = await _categoryService.EditCategoryAsync(id, categoryname,cancellation);

            return Ok(result);
        }

        [HttpDelete("/deleteCategory/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteAd(Guid id, string categoryname, CancellationToken cancellation)
        {
            await _categoryService.DeleteAsync(id,cancellation);
            return Ok();
        }
    }
}
