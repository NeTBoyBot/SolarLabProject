using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public string RecieverMail { get; set; }

        /// <summary>
        /// Содержимое темы письма
        /// </summary>
        [Required]
        public string Subject { get; set; }

        /// <summary>
        /// Содержимое письма
        /// </summary>
        [Required]
        public string Data { get; set; }
    }
}
