using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts.Mail
{
    public class InfoMailResponse
    {
        /// <summary>
        /// email отправителя
        /// </summary>
        public string SenderMail { get; set; }

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

        /// <summary>
        /// Успешно ли отправлено сообщение
        /// </summary>
        public bool IsSuccesfullySended { get; set; }
    }
}
