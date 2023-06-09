﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Board.Domain.Photos;

namespace Board.Domain
{
    public class User
    {
        #region userdata
        /// <summary>
        /// ID Пользователя
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Регион регистрации
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Язык пользователя
        /// </summary>
        public string Language { get; set; }

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
        /// ID Роли пользователя
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// Роль пользователя
        /// </summary>
        public Role Role { get; set; }
        #endregion

        #region collections
        /// <summary>
        /// Объявления принадлежащие пользователю
        /// </summary>
        public ICollection<Ad> Ads { get; set; }

        /// <summary>
        /// Отправленные сообщения
        /// </summary>
        public ICollection<Message> SendedMessages { get; set; }

        /// <summary>
        /// Полученные сообщения
        /// </summary>
        public ICollection<Message> RecievedMessages { get; set; }

        /// <summary>
        /// Избранные объявления пользователя
        /// </summary>
        public ICollection<FavoriteAd> FavoriteAds { get; set; }

        /// <summary>
        /// Отзывы оставленные пользователю
        /// </summary>
        public ICollection<Comment> SendedComments { get; set; }


        /// <summary>
        /// Отзывы оставленные пользователю
        /// </summary>
        public ICollection<Comment> RecievedComments { get; set; }

        /// <summary>
        /// Фотографии пользователя
        /// </summary>
        public ICollection<UserPhoto> Photos { get; set; }
        #endregion

        #region verification
        /// <summary>
        /// Является ли аккаунт пользователя подтверждённым
        /// </summary>
        public bool IsVerified { get;set; }

        /// <summary>
        /// Код для подтверждения аккаунта пользователя
        /// </summary>
        public int VerificationCode { get; set; }
        #endregion
    }
}
