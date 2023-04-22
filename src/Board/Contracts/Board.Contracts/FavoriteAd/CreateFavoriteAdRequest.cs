using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts.FavoriteAd
{
    public class CreateFavoriteAdRequest
    {
        /// <summary>
        /// ID Пользователя добавившего объявление в избранные
        /// </summary>
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        /// ID Объявления
        /// </summary>
        [Required]
        public Guid AdId { set; get; }
    }
}
