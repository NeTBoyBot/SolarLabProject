using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Domain.Photos
{
    public class UserPhoto
    {
        /// <summary>
        /// Id фотографии
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Содержимое фотографии представленное в виде Base64
        /// </summary>
        public string Base64 { get; set; }

        /// <summary>
        /// Id пользователя
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public User User { get; set; }
    }
}
