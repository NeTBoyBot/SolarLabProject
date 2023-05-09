using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts.Comment
{
    public class CreateCommentRequest
    {

        /// <summary>
        /// ID Пользователя которому оставили отзыв
        /// </summary>
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        /// Содержимое отзыва
        /// </summary>
        [MinLength(1)]
        [Required]
        public string Text { get; set; }
    }
}
