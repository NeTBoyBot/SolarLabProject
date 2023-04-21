using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
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
        public string Name { get; set; }

        /// <summary>
        /// Содержимое файла
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// Тип данных файла
        /// </summary>
        public string ContentType { get; set; }
    }
}
