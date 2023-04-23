using Board.Contracts.Ad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts.Photo.AdPhoto
{
    public class InfoAdPhotoResponse
    {
        /// <summary>
        /// Id фотографии
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Id Объявления
        /// </summary>
        public Guid AdId { get; set; }
    }
}
