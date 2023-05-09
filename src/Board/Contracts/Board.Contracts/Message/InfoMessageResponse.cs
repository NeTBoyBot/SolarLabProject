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
        public Guid SenderId { get; set; }

        /// <summary>
        /// Получатель
        /// </summary>
        public Guid RecieverId { get; set; }

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
