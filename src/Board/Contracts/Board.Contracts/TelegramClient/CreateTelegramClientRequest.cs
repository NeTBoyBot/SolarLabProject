using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts.TelegramClient
{
    public class CreateTelegramClientRequest
    {
        /// <summary>
        /// ID Аккаунта в telegram
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// ID Связанного аккаунта
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// ID Чата аккаунта telegram
        /// </summary>
        public long ChatId { get; set; }
    }
}
