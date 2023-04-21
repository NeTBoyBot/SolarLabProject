using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts.File
{
    public class InfoFileResponse
    {
        /// <summary>
        /// Id файла
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя файла
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Содержимое файла
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// Тип данных файла
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Размер файла
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Дата создания файла
        /// </summary>
        public DateTime CreationDate { get; set; }
    }
}
