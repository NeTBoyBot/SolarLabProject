using Board.Contracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts.Ad
{
    public class CreateAdRequest
    {
        /// <summary>
        /// ID Создателя объявления
        /// </summary>
        public Guid OwnerId { get; set; }

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
        /// ID Категории
        /// </summary>
        public Guid CategoryId { get; set; }
    }
}
