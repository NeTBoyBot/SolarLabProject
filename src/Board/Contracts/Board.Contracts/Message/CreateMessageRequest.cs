using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts.Message
{
    public class CreateMessageRequest
    {

        /// <summary>
        /// ID Получателя
        /// </summary>
        [Required]
        public Guid RecieverId { get; set; }


        /// <summary>
        /// Содержимое сообщения
        /// </summary>
        [Required]
        public string Containment { get; set; }

    }
}
