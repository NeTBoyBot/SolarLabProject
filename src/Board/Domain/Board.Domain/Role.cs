using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Domain
{
    public class Role
    {
        /// <summary>
        /// ID Роли
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя роли
        /// </summary>
        public string RoleName { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
