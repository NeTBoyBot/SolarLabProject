using Board.Contracts.Ad;
using Board.Contracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts.FavoriteAd
{
    public class InfoFavoriteAdResponse
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
        public InfoUserResponse User { get; set; }

        /// <summary>
        /// ID Объявления
        /// </summary>
        public Guid AdId { set; get; }

        /// <summary>
        /// Объявление
        /// </summary>
        public InfoAdResponse Ad { get; set; }
    }
}
