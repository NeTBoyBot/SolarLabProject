using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts.Category
{
    public class InfoCategoryResponse
    {
        /// <summary>
        /// ID Категории
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя категории
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Коллекция под категорий
        /// </summary>
        public ICollection<InfoCategoryResponse> SubCategories;

#nullable enable
        /// <summary>
        /// Родительская категория
        /// </summary>
        public Guid? ParentCategoryId { get; set; }

    }
}
