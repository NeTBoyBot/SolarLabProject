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

        public Task<InfoMailResponse> SendVerificationCodeAsync(Guid userId,string email,int Code,CancellationToken cancellation);

    }
}
