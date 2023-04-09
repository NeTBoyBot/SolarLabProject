using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts.Message
{
    public class CreateMessageRequest
    {

        /// <summary>
        /// ID Отправителя
        /// </summary>
        public Guid SenderId { get; set; }


        /// <summary>
        /// ID Получателя
        /// </summary>
        public Guid RecieverId { get; set; }


        /// <summary>
        /// Содержимое сообщения
        /// </summary>
        public string Containment { get; set; }

    }
}
