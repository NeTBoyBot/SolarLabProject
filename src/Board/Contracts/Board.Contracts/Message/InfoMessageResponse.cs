using Board.Contracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts.Message
{
    public class InfoMessageResponse
    {
        /// <summary>
        /// ID Сообщения
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Отправитель
        /// </summary>
        public InfoUserResponse Sender { get; set; }

        /// <summary>
        /// Получатель
        /// </summary>
        public InfoUserResponse Reciever { get; set; }

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
