using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts.Mail
{
    public class SendMailRequest
    {
        /// <summary>
        /// email получателя
        /// </summary>
        public string RecieverMail { get; set; }

        /// <summary>
        /// Содержимое темы письма
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Содержимое письма
        /// </summary>
        public string Data { get; set; }
    }
}
