using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts.User
{
    public class RegisterUserRequest
    {

        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [Required]
        public string Password { get; set; }

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
        /// Адрес электронной почты
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Язык пользователя
        /// </summary>
        [Required]
        public string Language { get; set; }

    }
}
