using Board.Contracts.Ad;
using Board.Contracts.FavoriteAd;
using Doska.AppServices.Services.FavoriteAd;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Doska.API.Controllers
{
    [ApiController]
    public class FavoriteAdController : ControllerBase
    {
        IFavoriteAdService _favoriteadService;
        public FavoriteAdController(IFavoriteAdService adService)
        {
            _favoriteadService = adService;
        }
        [HttpGet("/allFavoriteAds")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoFavoriteAdResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(int take, int skip)
        {
            var result = await _favoriteadService.GetAll(take, skip);

            return Ok(result);
        }

        [HttpPost("/createFavoriteAd")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoFavoriteAdResponse>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateAd(CreateFavoriteAdRequest request, CancellationToken cancellation)
        {
            var result = await _favoriteadService.CreateFavoriteAdAsync(request,cancellation);

            return Created("", result);
        }

        [HttpDelete("/deleteFavoriteAd/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteAd(Guid id, InfoFavoriteAdResponse request, CancellationToken cancellation)
        {
            await _favoriteadService.DeleteAsync(id,cancellation);
            return Ok();
        }

        [HttpGet("/allUserFavorites")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoFavoriteAdResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllUserFavorites(int take, int skip, CancellationToken token)
        {
            var result = await _favoriteadService.GetAllUserFavorites(take, skip, token);

            return Ok(result);
        }
    }
}
