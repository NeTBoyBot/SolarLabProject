using Board.Contracts.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doska.AppServices.Services.User
{
    public interface IUserService
    {
        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="RegisterUserRequest"> Дто с логином, паролем, почтой и регионом</param>
        /// <returns></returns>
        public Task<RegisterUserResponse> Register(RegisterUserRequest RegisterUserRequest,CancellationToken cancellationToken);

        /// <summary>
        /// Логин пользователя
        /// </summary>
        /// <param name="LoginUserRequest"></param>
        /// <returns>Токен</returns>
        public Task<string> Login(LoginUserRequest LoginUserRequest, CancellationToken cancellationToken);

        /// <summary>
        /// Получить текущего пользователя
        /// </summary>
        /// <returns></returns>
        Task<InfoUserResponse> GetCurrentUser(CancellationToken cancellationToken);

        /// <summary>
        /// Подтверждение аккаунта пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <param name="VerificationCode"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> VerifyUserAsync(Guid id,int VerificationCode,CancellationToken cancellationToken);

        /// <summary>
        /// Получить id авторизованного пользователя
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Guid> GetCurrentUserId(CancellationToken cancellationToken);

        /// <summary>
        /// Получить пользователя по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<InfoUserResponse> GetByIdAsync(Guid id, CancellationToken cancellation);

        /// <summary>
        /// Создание пользователя
        /// </summary>
        /// <param name="registerUser"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<Guid> CreateUserAsync(RegisterUserRequest registerUser, CancellationToken cancellation);

        /// <summary>
        /// Получение всех пользователей
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        Task<IReadOnlyCollection<InfoUserResponse>> GetAll(int take, int skip);

        /// <summary>
        /// Удаление пользователя по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id, CancellationToken cancellation);

        /// <summary>
        /// Обновление информации о пользователе
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="editAd"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<InfoUserResponse> EditUserAsync(Guid Id, EditUserRequest editAd, CancellationToken cancellation);

        /// <summary>
        /// Проверка является ли аккаунт авторизованного пользователя верифицированным
        /// </summary>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<bool> IsUserVerified(CancellationToken cancellation);

        /// <summary>
        /// Смена пароля пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <param name="oldpass"></param>
        /// <param name="newpass"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public Task<InfoUserResponse> ChangeUserPassword(Guid id, string newpass, CancellationToken cancellation);

        /// <summary>
        /// Сравнение переданного пароля с паролем авторизованного пользователя
        /// </summary>
        /// <param name="pass"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public Task<bool> ComparePasswords(string pass, CancellationToken cancellation);
    }
}
