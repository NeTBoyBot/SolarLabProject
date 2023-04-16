using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Domain
{
    public class Message
    {
        /// <summary>
        /// ID Сообщения
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// ID Отправителя
        /// </summary>
        public Guid SenderId { get; set; }

        /// <summary>
        /// Отправитель
        /// </summary>
        public User Sender { get; set; }

        /// <summary>
        /// ID Получателя
        /// </summary>
        public Guid RecieverId { get; set; }

        /// <summary>
        /// Получатель
        /// </summary>
        public User Reciever { get; set; }

        /// <summary>
        /// Содержимое сообщения
        /// </summary>
        public string Containment { get; set; }

        /// <summary>
        /// Дата отправки
        /// </summary>
        public DateTime SendDate { get; set; }


    }
}
