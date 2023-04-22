using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts.File
{
    public class CreateFileRequest
    {
        /// <summary>
        /// Имя файла
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Содержимое файла
        /// </summary>
        [Required]
        public byte[] Data { get; set; }

        /// <summary>
        /// Тип данных файла
        /// </summary>
        [Required]
        public string ContentType { get; set; }
    }
}
