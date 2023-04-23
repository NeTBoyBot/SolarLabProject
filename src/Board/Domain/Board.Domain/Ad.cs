using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Board.Domain.Photos;

namespace Board.Domain
{
    public class Ad
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
        /// ID Создателя объявления
        /// </summary>
        public Guid OwnerId { get; set; }

        /// <summary>
        /// Владелец объявления
        /// </summary>
        public User Owner { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Категория объявления
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        /// ID Категории
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Язык объявления
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Фотографии объявления
        /// </summary>
        public ICollection<AdPhoto> Photos { get; set; }
    }
}
