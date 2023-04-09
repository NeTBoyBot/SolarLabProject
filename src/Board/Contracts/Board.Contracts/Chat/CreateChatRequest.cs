using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts.Chat
{
    public class CreateChatRequest
    {
        /// <summary>
        /// ID Чата
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// ID Создателя чата
        /// </summary>
        public Guid CreatorId { get; set; }

        /// <summary>
        /// ID Участника чата
        /// </summary>
        public Guid MemberId { get; set; }

    }
}
