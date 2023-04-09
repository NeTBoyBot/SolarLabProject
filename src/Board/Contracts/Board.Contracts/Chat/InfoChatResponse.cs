using Board.Contracts.Message;
using Board.Contracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts.Chat
{
    public class InfoChatResponse
    {
        /// <summary>
        /// ID Чата
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Создатель чата
        /// </summary>
        public InfoUserResponse Creator { get; set; }

        /// <summary>
        /// Участник чата
        /// </summary>
        public InfoUserResponse Member { get; set; }

        /// <summary>
        /// Сообщения в чате
        /// </summary>
        public ICollection<InfoMessageResponse> Messages { get; set; }
    }
}
