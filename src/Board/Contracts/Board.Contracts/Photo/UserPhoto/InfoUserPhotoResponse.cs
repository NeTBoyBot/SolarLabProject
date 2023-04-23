using Board.Contracts.Ad;
using Board.Contracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts.Photo.AdPhoto
{
    public class InfoUserPhotoResponse
    {
        /// <summary>
        /// Id фотографии
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Id Пользователя
        /// </summary>
        public Guid UserId { get; set; }
    }
}
