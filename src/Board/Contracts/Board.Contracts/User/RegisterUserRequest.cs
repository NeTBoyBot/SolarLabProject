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
        [MinLength(3)]
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [MinLength(8)]
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Регион регистрации
        /// </summary>
        [MinLength(2)]
        [Required]
        public string Region { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        [MinLength(7)]
        [Required]
        [Phone]
        public string Phone { get; set; }

        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        [MinLength(3)]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Язык пользователя
        /// </summary>
        [Required]
        public string Language { get; set; }

        /// <summary>
        /// ID Роли
        /// </summary>
        public string RoleName { get; set; }

    }
}
