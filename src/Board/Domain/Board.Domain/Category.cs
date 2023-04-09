using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Domain
{
    public class Category
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
        public ICollection<Category> SubCategories;

#nullable enable
        /// <summary>
        /// Родительская категория
        /// </summary>
        public Category? ParentCategory { get; set; }

        /// <summary>
        /// ID Родительской категории
        /// </summary>
        public Guid? ParentCategoryId { get; set; }
    }
}
