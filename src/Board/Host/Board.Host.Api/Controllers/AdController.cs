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
        /// <summary>
        /// Получение списка всех объявлений
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpGet("/all")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoAdResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(int take, int skip,CancellationToken cancellation)
        {

            var result = await _adService.GetAll(take, skip);

            return Ok(result);
        }

        /// <summary>
        /// Получение списка всех объявлений переведенных на язык пользователя
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpGet("/allTranslated")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoAdResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllTranslated(int take, int skip, CancellationToken cancellation)
        {
            var user = await _userService.GetCurrentUser(cancellation);

            var result = await _adService.GetAllTranslated(take, skip, user.Language);

            return Ok(result);
        }

        /// <summary>
        /// Получение объявления по фильтру
        /// </summary>
        /// <param name="name"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet("/GetAdFiltered")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoAdResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAdFiltered(string? name, Guid? categoryId)
        {
            var result = await _adService.GetAdFiltered(name,categoryId);

            return Ok(result);
        }

        /// <summary>
        /// Создание объявления
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost("/createAd")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoAdResponse>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateAd(CreateAdRequest request,CancellationToken cancellation)
        {
            if (!await _userService.IsUserVerified(cancellation))
                throw new Exception("Аккаунт пользователя не подтверждён!");

            var user = await _userService.GetCurrentUser(cancellation);

            

            var result = await _adService.CreateAdAsync(user.Id,user.Language,request,cancellation);

            return Created("",result);
        }

        /// <summary>
        /// Обновление объявления
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPut("/updateAd/{id}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoAdResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateAd(Guid id,CreateAdRequest request, CancellationToken cancellation)
        {
            if (!await _userService.IsUserVerified(cancellation))
                throw new Exception("Аккаунт пользователя не подтверждён!");

            var result = await _adService.EditAdAsync(id,request,cancellation);

            return Ok(result);
        }

        /// <summary>
        /// Удаление объявления
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpDelete("/deleteAd/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteAd(Guid id, CancellationToken cancellation)
        {
            if (!await _userService.IsUserVerified(cancellation))
                throw new Exception("Аккаунт пользователя не подтверждён!");

            await _adService.DeleteAsync(id,cancellation);
            return Ok();
        }


        /// <summary>
        /// Получение всех объявлений авторизованного пользователя
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("/allUserAds")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoAdResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllUserAds(int take, int skip,CancellationToken token)
        {
            var result = await _adService.GetAllUserAds(take, skip,token);

            return Ok(result);
        }
    }
}
