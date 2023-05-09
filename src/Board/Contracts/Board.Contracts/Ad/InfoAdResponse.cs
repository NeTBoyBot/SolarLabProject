using Board.Contracts.Category;
using Board.Contracts.Photo.AdPhoto;
using Board.Contracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts.Ad
{
    public class InfoAdResponse
    {
        /// <summary>
        /// ID Объявления
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя объявления
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание объявления
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// Стоимость объявления
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Владелец объявления
        /// </summary>
        public Guid OwnerId { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Категория объявления
        /// </summary>
        public InfoCategoryResponse Category { get; set; }

        /// <summary>
        /// Фотографии объявления
        /// </summary>
        public ICollection<InfoAdPhotoResponse> Photos { get; set; }
    }
}
