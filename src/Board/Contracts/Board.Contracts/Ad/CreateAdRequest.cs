using Board.Contracts.User;
using Microsoft.AspNetCore.Http;
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
        [MinLength(5)]
        public string Name { get; set; }

        /// <summary>
        /// Описание объявления
        /// </summary>
        [MinLength(10)]
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
