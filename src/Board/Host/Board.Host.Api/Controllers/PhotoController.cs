using Board.Application.AppData.Contexts.Photo;
using Board.Contracts.Ad;
using Board.Contracts.Photo.AdPhoto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Net;

namespace Board.Host.Api.Controllers
{
    [ApiController]
    public class PhotoController : ControllerBase
    {
        public readonly IPhotoService _photoService;

        public PhotoController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        /// <summary>
        /// Получение всех фото пользователей
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoUserPhotoResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadGateway)]
        [HttpGet("GetAllUserPhotos")]
        public async Task<IActionResult> GetAllUserPhotos(int take, int skip, CancellationToken cancellation)
        {
            var result = await _photoService.GetAllUserPhotos(take, skip);

            return Ok(result);
        }

        /// <summary>
        /// Получение всех фото объявлений
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoAdPhotoResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadGateway)]
        [HttpGet("GetAllAdPhotos")]
        public async Task<IActionResult> GetAllAdPhotos(int take, int skip, CancellationToken cancellation)
        {
            var result = await _photoService.GetAllAdPhotos(take, skip);

            return Ok(result);
        }

        /// <summary>
        /// Загрузка фото пользователя
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="file"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadGateway)]
        [HttpPost("UploadUserPhoto")]
        public async Task<IActionResult> UploadUserPhoto(Guid UserId, IFormFile file, CancellationToken cancellation)
        {
            byte[] photo;
            await using (var ms = new MemoryStream())
            await using (var fs = file.OpenReadStream())
            {
                await fs.CopyToAsync(ms);
                photo = ms.ToArray();
            }
           

            var result = await _photoService.CreateUserPhotoAsync(UserId,photo, cancellation);

            return Ok(result);
        }

        /// <summary>
        /// Загрузка фото объявления
        /// </summary>
        /// <param name="AdId"></param>
        /// <param name="file"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadGateway)]
        [HttpPost("UploadAdPhoto")]
        public async Task<IActionResult> UploadAdPhoto(Guid AdId, IFormFile file, CancellationToken cancellation)
        {
            byte[] photo;
            await using (var ms = new MemoryStream())
            await using (var fs = file.OpenReadStream())
            {
                await fs.CopyToAsync(ms);
                photo = ms.ToArray();
            }

            var result = await _photoService.CreateAdPhotoAsync(AdId,photo, cancellation);

            return Ok(result);
        }

        /// <summary>
        /// Удаление фото объявления
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpDelete("DeleteAdPhoto")]
        public async Task<IActionResult> DeleteAdPhoto(Guid Id, CancellationToken cancellation)
        {
            await _photoService.DeleteAdPhotoAsync(Id, cancellation);

            return Ok();
        }

        /// <summary>
        /// Удаление фото пользователя
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpDelete("DeleteUserPhoto")]
        public async Task<IActionResult> DeleteUserPhoto(Guid Id, CancellationToken cancellation)
        {
            await _photoService.DeleteUserPhotoAsync(Id, cancellation);

            return Ok();
        }

        /// <summary>
        /// Получение фото пользователя по Id
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(InfoUserPhotoResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet("GetUserPhotoById")]
        public async Task<IActionResult> GetUserPhotoById(Guid Id, CancellationToken cancellation)
        {
            var result = await _photoService.GetUserPhotoByIdAsync(Id, cancellation);

            return Ok(result);
        }

        /// <summary>
        /// Получение фото объявления по Id
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(InfoAdPhotoResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet("GetAdPhotoById")]
        public async Task<IActionResult> GetAdPhotoById(Guid Id, CancellationToken cancellation)
        {
            var result = await _photoService.GetAdPhotoByIdAsync(Id, cancellation);

            return Ok(result);
        }

    }
}
