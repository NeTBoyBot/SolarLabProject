using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Domain
{
    public class FavoriteAd
    {
        /// <summary>
        /// Id Избранного объявления
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// ID Пользователя добавившего объявление в избранные
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Пользователь добавивший объявление в избранные
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// ID Объявления
        /// </summary>
        public Guid AdId { set; get; }

        /// <summary>
        /// Объявление
        /// </summary>
        public Ad Ad { get; set; }
    }
}
