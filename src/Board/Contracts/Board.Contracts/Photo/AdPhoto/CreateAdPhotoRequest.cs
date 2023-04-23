using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts.Photo.AdPhoto
{
    public class CreateAdPhotoRequest
    {
        /// <summary>
        /// Содержимое фотографии представленное в виде Base64
        /// </summary>
        public string Base64 { get; set; }

        /// <summary>
        /// Id Объявления
        /// </summary>
        public Guid AdId { get; set; }

    }
}
