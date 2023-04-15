using Board.Contracts.Mail;
using Board.Domain;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using MimeKit;
using MailKit.Net.Smtp;
using System.Text;
using System.Threading.Tasks;
using Doska.AppServices.Services.User;
<<<<<<< Updated upstream
using Board.Application.AppData.Contexts.User;
=======
using Doska.AppServices.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using System.Web;
>>>>>>> Stashed changes

namespace Board.Application.AppData.Contexts.Mail
{
    public class MailService : IMailService
    {
        public IUserService _userService;
        public IUserRepository _userRepository;
        public IConfiguration _configuration;

        public MailService(IUserService userService,IUserRepository userRepository, IConfiguration configuration)
        {
            _userService = userService;
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<InfoMailResponse> SendVerificationCodeAsync(Guid userId,string email,int Code,CancellationToken cancellation)
        {
            //var currentUser = await _userRepository.FindById((await _userService.GetCurrentUser(cancellation)).Id,cancellation);
            var subject = "Verification mail";
            //var message = $"Your verification code is {Code}";

            var message = $"Подтвердите регистрацию, перейдя по ссылке: <a href='https://localhost:5001/VerifyUser?userId={userId}'>link</a>";
            try
            {
                
                using var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress("SolarVerification", _configuration["Mail:Address"]));
                emailMessage.To.Add(new MailboxAddress("", email));
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = message
                };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.mail.ru", 587, false);
                    await client.AuthenticateAsync(_configuration["Mail:Address"], _configuration["Mail:Pass"]);
                    await client.SendAsync(emailMessage);

                    await client.DisconnectAsync(true);
                }
                return new InfoMailResponse
                {
                    SenderMail = "s.verify@mail.ru",
                    RecieverMail = email,
                    Subject = "",
                    Data = subject,
                    IsSuccesfullySended = true

                };
            }
            catch
            {
                return new InfoMailResponse
                {
                    SenderMail = "s.verify@mail.ru",
                    RecieverMail = email,
                    Subject = subject,
                    Data = message,
                    IsSuccesfullySended = false
                };
            }
        }
    }
}
