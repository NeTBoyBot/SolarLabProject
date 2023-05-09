using Board.Application.AppData.Contexts.Photo;
using Board.Contracts.Ad;
using Board.Contracts.FavoriteAd;
using Doska.AppServices.Services.Ad;
using Doska.AppServices.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Doska.API.Controllers
{
    //TODO Test Authorize Attribute
    //[ApiController]
    public class AdController : ControllerBase
    {
        private readonly IAdService _adService;
        private readonly IUserService _userService;
        private readonly IPhotoService _photoService;
        public AdController(IAdService adService,IUserService userService, IPhotoService photoService)
        {
            _adService = adService;
            _userService = userService;
            _photoService = photoService;
        }

        #region Ads
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
        public async Task<IActionResult> CreateAd(CreateAdRequest request, List<IFormFile> Photos ,CancellationToken cancellation)
        {
            if (!await _userService.IsUserVerified(cancellation))
                throw new Exception("Аккаунт пользователя не подтверждён!");

            var user = await _userService.GetCurrentUser(cancellation);

            var result = await _adService.CreateAdAsync(user.Id,user.Language,request,cancellation);

            foreach(var file in Photos)
            {
                byte[] photo;
                await using (var ms = new MemoryStream())
                await using (var fs = file.OpenReadStream())
                {
                    await fs.CopyToAsync(ms);
                    photo = ms.ToArray();
                }

                await _photoService.CreateAdPhotoAsync(result,photo,cancellation);
            }

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
        #endregion


        #region FavoriteAds
        /// <summary>
        /// Получение всех избранных объявлений
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        [HttpGet("/allFavoriteAds")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoFavoriteAdResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllFavorites(int take, int skip)
        {
            var result = await _adService.GetAllFavorites(take, skip);

            return Ok(result);
        }

        /// <summary>
        /// Добавить объявление в избранное
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpPost("/createFavoriteAd")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoFavoriteAdResponse>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateAd(CreateFavoriteAdRequest request, CancellationToken cancellation)
        {
            var result = await _adService.CreateFavoriteAdAsync(request, cancellation);

            return Created("", result);
        }

        /// <summary>
        /// Удалить объявление из избранных
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpDelete("/deleteFavoriteAd/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteAd(Guid id, InfoFavoriteAdResponse request, CancellationToken cancellation)
        {
            await _adService.DeleteFavoriteAdAsync(id, cancellation);
            return Ok();
        }

        /// <summary>
        /// Получить все избранные объявления авторизованного пользователя
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("/allUserFavorites")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoFavoriteAdResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllUserFavorites(int take, int skip, CancellationToken token)
        {
            var result = await _adService.GetAllUserFavorites(take, skip, token);

            return Ok(result);
        }
        #endregion
    }
}
