using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts.Photo.AdPhoto
{
    public class CreateUserPhotoRequest
    {
        /// <summary>
        /// Содержимое фотографии представленное в виде Base64
        /// </summary>
        public string Base64 { get; set; }

        /// <summary>
        /// Id Пользователя
        /// </summary>
        public Guid UserId { get; set; }

    }
}
