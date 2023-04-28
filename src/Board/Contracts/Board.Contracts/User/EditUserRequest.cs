using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts.User
{
    public class EditUserRequest
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Регион регистрации
        /// </summary>
        [Required]
        public string Region { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        [Required]
        [Phone]
        public string Phone { get; set; }

        /// <summary>
        /// Язык пользователя
        /// </summary>
        [Required]
        public string Language { get; set; }
    }
}
