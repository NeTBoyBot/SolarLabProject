using Board.Contracts.Chat;
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

        /// <summary>
        /// ID Чата в который было отправлено сообщение 
        /// </summary>
        public Guid ChatId { get; set; }

        /// <summary>
        /// Чат в который было отправлено сообщение
        /// </summary>
        public InfoChatResponse Chat { get; set; }
    }
}
