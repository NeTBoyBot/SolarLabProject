using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Domain
{
    public class Chat
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
        /// Создатель чата
        /// </summary>
        public User Creator { get; set; }

        /// <summary>
        /// ID Участника чата
        /// </summary>
        public Guid MemberId { get; set; }

        /// <summary>
        /// Участник чата
        /// </summary>
        public User Member { get; set; }

        /// <summary>
        /// Сообщения в чате
        /// </summary>
        public ICollection<Message> Messages { get; set; }
    }
}
