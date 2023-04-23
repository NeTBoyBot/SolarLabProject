
using Board.Application.AppData.Contexts.Mail;
using Board.Contracts.User;
using Doska.AppServices.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Doska.API.Controllers
{
    [AllowAnonymous]
    //[ApiController]
    public class UserController : ControllerBase
    {
        IUserService _userService;
        IMailService _mailService;
        public UserController(IUserService userService, IMailService mailService)
        {
            _userService = userService;
            _mailService = mailService;
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
        public async Task<IActionResult> Register(RegisterUserRequest request, CancellationToken token)
        {
            var user = await _userService.Register(request, token);

            await _mailService.SendVerificationCodeAsync(user.Id,request.Email,user.VerificationCode, token);

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
        public async Task<IActionResult> login(LoginUserRequest request, CancellationToken Canctoken)
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
        /// <param name="request"></param>
        /// <param name="cancellation"></param>
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
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpPut("/updateUser/{id}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoUserResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateUser(Guid id, RegisterUserRequest request, CancellationToken cancellation)
        {
            var result = await _userService.EditUserAsync(id, request,cancellation);

            return Ok(result);
        }

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
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
        /// <param name="token"></param>
        /// <returns></returns>
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
        /// <param name="token"></param>
        /// <returns></returns>
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
        /// <param name="userId"></param>
        /// <param name="Code"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpGet("/VerifyUser")]
        public async Task<IActionResult> VerifyUser(Guid userId,int Code,CancellationToken cancellation)
        {
            var user = await _userService.GetCurrentUser(cancellation);
            var result = await _userService.VerifyUserAsync(userId, Code, cancellation);

            return Ok(result);
        }
    }
}
