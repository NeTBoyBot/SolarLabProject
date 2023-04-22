using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts.Category
{
    public class CreateCategoryRequest
    {
        [Required]
        /// <summary>
        /// Имя категории
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ID Родительской категории
        /// </summary>
        public Guid? ParentCategoryId { get; set; }
    }
}
