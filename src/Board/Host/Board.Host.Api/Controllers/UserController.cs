
using Board.Application.AppData.Contexts.Mail;
using Board.Application.AppData.Contexts.Photo;
using Board.Contracts.User;
using Doska.AppServices.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.OleDb;
using System.Net;

namespace Doska.API.Controllers
{
    [AllowAnonymous]
    //[ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserService _userService;
        public readonly IMailService _mailService;
        public readonly IPhotoService _photoService;

        public UserController(IUserService userService, IMailService mailService,IPhotoService photoService)
        {
            _userService = userService;
            _mailService = mailService;
            _photoService = photoService;
        }

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="request"></param>
        /// <param name="file"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("/Register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Register(RegisterUserRequest request,IFormFile file, CancellationToken token)
        {
            var user = await _userService.Register(request, token);

            byte[] photo;
            
            if (file==null || file.Length == 0)
                photo = new byte[0];
            else
            {
                await using (var ms = new MemoryStream())
                await using (var fs = file.OpenReadStream())
                {
                    await fs.CopyToAsync(ms);
                    photo = ms.ToArray();
                }
            }

            await _mailService.SendVerificationCodeAsync(user.Id, request.Email, user.VerificationCode, token);

            await _photoService.CreateUserPhotoAsync(user.Id, photo, token);

            return Created("",user);
        }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        /// <param name="request"></param>
        /// <param name="Canctoken"></param>
        /// <returns></returns>
        [HttpPost("/Login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Login(LoginUserRequest request, CancellationToken Canctoken)
        {
            var token = await _userService.Login(request, Canctoken);
            return Created("", token);
        }

        /// <summary>
        /// Получение всех пользователей
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        [HttpGet("/allusers")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoUserResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(int take, int skip)
        {
            var result = await _userService.GetAll(take, skip);

            return Ok(result);
        }

        /// <summary>
        /// Создание пользователя
        /// </summary>
        /// <param name="request">Данные для регистрации</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns></returns>
        [HttpPost("/createUser")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoUserResponse>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateUser(RegisterUserRequest request, CancellationToken cancellation)
        {
            var result = await _userService.CreateUserAsync(request,cancellation);

            return Created("", result);
        }

        /// <summary>
        /// Обновление ифнормации о пользователе
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <param name="request">Новые данные</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns></returns>
        [HttpPut("/updateUser/{id}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoUserResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateUser(Guid id, EditUserRequest request, CancellationToken cancellation)
        {
            var result = await _userService.EditUserAsync(id, request,cancellation);

            return Ok(result);
        }

        /// <summary>
        /// Отправка ссылки для изменения пароля текущего пользователя
        /// </summary>
        /// <param name="oldpass">Старый пароль</param>
        /// <param name="newpass">Новый пароль</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns></returns>
        [HttpGet("/sendUpdatePasswordMessage")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoUserResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> SendUpdatePasswordMessage(string oldpass,string newpass, CancellationToken cancellation)
        {
            var user = await _userService.GetCurrentUser(cancellation);

            if (!await _userService.ComparePasswords(oldpass, cancellation))
                return NotFound();

            //var result = await _userService.ChangeUserPassword(user.Id, oldpass,newpass, cancellation);

            await _mailService.SendChangePasswordLinkAsync(user.Id, newpass, user.Email, cancellation);

            return Ok("Сообщение с дальнейшими инструкциями было отправлено на вашу почту");
        }

        /// <summary>
        /// Обновление пароля пользователя
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <param name="newpass">Новый пароль</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns></returns>
        [HttpGet("/updatePassword")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoUserResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdatePassword(Guid id,string newpass, CancellationToken cancellation)
        {
            var result = await _userService.ChangeUserPassword(id, newpass, cancellation);

            return Ok(result);
        }

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="id">ID пользователя</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns></returns>
        [HttpDelete("/deleteUser/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteUser(Guid id, CancellationToken cancellation)
        {
            await _userService.DeleteAsync(id,cancellation);
            return Ok();
        }

        /// <summary>
        /// Получение авторизованного пользователя
        /// </summary>
        /// <param name="token">Токен отмены</param>
        /// <returns>Информация об авторизованном пользователе</returns>
        [HttpGet("/CurrentUser")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoUserResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCurentUser(CancellationToken token)
        {
            var result = await _userService.GetCurrentUser(token);

            return Ok(result);
        }

        /// <summary>
        /// Получение id авторизованного пользователя
        /// </summary>
        /// <param name="token">Токен отмены</param>
        /// <returns>ID авторизованного пользователя</returns>
        [HttpGet("/CurrentUserId")]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCurentUserId(CancellationToken token)
        {
            var result = await _userService.GetCurrentUserId(token);

            return Ok(result);
        }

        /// <summary>
        /// Подтверждение аккаунта пользователя
        /// </summary>
        /// <param name="userId">ID пользователя для подтверждения</param>
        /// <param name="Code">Код для подтверждения</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns>Информация о том подтвержден ли аккаунт</returns>
        [HttpGet("/VerifyUser")]
        public async Task<IActionResult> VerifyUser(Guid userId,int Code,CancellationToken cancellation)
        {
            var user = await _userService.GetCurrentUser(cancellation);
            var result = await _userService.VerifyUserAsync(userId, Code, cancellation);

            return Ok(result);
        }
    }
}
