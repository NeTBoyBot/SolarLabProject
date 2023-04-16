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
        public Task<RegisterUserResponse> Register(RegisterUserRequest RegisterUserRequest, byte[] file,CancellationToken cancellationToken);

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

        Task<string> VerifyUserAsync(Guid id,int VerificationCode,CancellationToken cancellationToken);

        Task<Guid> GetCurrentUserId(CancellationToken cancellationToken);

        Task<InfoUserResponse> GetByIdAsync(Guid id, CancellationToken cancellation);

        Task<Guid> CreateUserAsync(RegisterUserRequest registerUser, CancellationToken cancellation);

        Task<IReadOnlyCollection<InfoUserResponse>> GetAll(int take, int skip);

        Task DeleteAsync(Guid id, CancellationToken cancellation);

        Task<InfoUserResponse> EditUserAsync(Guid Id, RegisterUserRequest editAd, CancellationToken cancellation);

        Task<bool> IsUserVerified(CancellationToken cancellation);

    }
}
