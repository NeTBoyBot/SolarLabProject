using Board.Contracts.Photo.AdPhoto;
using Board.Contracts.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts.User
{
    public class InfoUserResponse
    {
        /// <summary>
        /// ID Пользователя
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Регион регистрации
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Дата создания аккаунта
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// Является ли аккаунт пользователя подтверждённым
        /// </summary>
        public bool IsVerified { get; set; }

        /// <summary>
        /// Язык пользователя
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Фотографии пользователя
        /// </summary>
        public ICollection<InfoUserPhotoResponse> Photos { get; set; }

        /// <summary>
        /// Роль пользователя
        /// </summary>
        public InfoRoleResponse Role { get; set; }
    }
}
