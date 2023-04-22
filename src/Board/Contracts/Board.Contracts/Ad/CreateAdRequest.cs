using Board.Contracts.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts.Ad
{
    public class CreateAdRequest
    {
        /// <summary>
        /// Имя объявления
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Описание объявления
        /// </summary>
        [Required]
        public string Desc { get; set; }

        /// <summary>
        /// Стоимость объявления
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// ID Категории
        /// </summary>
        [Required]
        public Guid CategoryId { get; set; }
    }
}
