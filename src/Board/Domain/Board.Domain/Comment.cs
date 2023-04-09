﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Domain
{
    public class Comment
    {
        /// <summary>
        /// ID Отзыва
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// ID Пользователя оставившего отзыв
        /// </summary>
        public Guid SenderId { get; set; }

        /// <summary>
        /// Пользователь оставивший отзыв
        /// </summary>
        public User Sender { get; set; }

        /// <summary>
        /// ID Пользователя которому оставили отзыв
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Пользователь которому оставили отзыв
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Содержимое отзыва
        /// </summary>
        public string Text { get; set; }
    }
}
