using Board.Contracts.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.Mail
{
    public interface IMailService
    {
        /// <summary>
        /// Отправка кода подтверждения пользователю
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="email"></param>
        /// <param name="Code"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public Task<InfoMailResponse> SendVerificationCodeAsync(Guid userId,string email,int Code,CancellationToken cancellation);

        /// <summary>
        /// Отправка ссылки для смены пароля
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newpass"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public Task<InfoMailResponse> SendChangePasswordLinkAsync(Guid userId, string newpass,string email, CancellationToken cancellation);

    }
}
