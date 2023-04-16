using Board.Contracts.Ad;
using Doska.AppServices.Services.Ad;
using Doska.AppServices.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Doska.API.Controllers
{
    //TODO Test Authorize Attribute
    [ApiController]
    public class AdController : ControllerBase
    {
        public IAdService _adService;
        public IUserService _userService;
        public AdController(IAdService adService,IUserService userService)
        {
            _adService = adService;
            _userService = userService;
        }
        [HttpGet("/all")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoAdResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(int take, int skip)
        {
            var result = await _adService.GetAll(take, skip);

            return Ok(result);
        }

        [HttpGet("/GetAdFiltered")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoAdResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAdFiltered(string? name, Guid? categoryId)
        {
            var result = await _adService.GetAdFiltered(name,categoryId);

            return Ok(result);
        }

        [HttpPost("/createAd")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoAdResponse>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateAd(CreateAdRequest request,CancellationToken cancellation)
        {
            if (!await _userService.IsUserVerified(cancellation))
                throw new Exception("Аккаунт пользователя не подтверждён!");

            var userId = await _userService.GetCurrentUserId(cancellation);

            var result = await _adService.CreateAdAsync(userId,request,cancellation);

            return Created("",result);
        }

        [HttpPut("/updateAd/{id}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoAdResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateAd(Guid id,CreateAdRequest request, CancellationToken cancellation)
        {
            var result = await _adService.EditAdAsync(id,request,cancellation);

            return Ok(result);
        }

        [HttpDelete("/deleteAd/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteAd(Guid id, CancellationToken cancellation)
        {
            await _adService.DeleteAsync(id,cancellation);
            return Ok();
        }

        [HttpGet("/allUserAds")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoAdResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllUserAds(int take, int skip,CancellationToken token)
        {
            var result = await _adService.GetAllUserAds(take, skip,token);

            return Ok(result);
        }
    }
}
