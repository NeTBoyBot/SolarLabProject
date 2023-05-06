using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Domain
{
    public class TelegramClient
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
        /// Связанный аккаунт
        /// </summary>
        public User? User { get; set; }

        /// <summary>
        /// ID Чата аккаунта telegram
        /// </summary>
        public long ChatId { get; set; }
        
        /// <summary>
        /// Категории на которые подписан пользователь
        /// </summary>
        public ICollection<Category> SubscribedCategories { get; set; }
    }
}
