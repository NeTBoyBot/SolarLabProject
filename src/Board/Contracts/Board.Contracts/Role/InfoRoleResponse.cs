using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts.Role
{
    public class InfoRoleResponse
    {
        /// <summary>
        /// ID Роли
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// Имя роли
        /// </summary>
        public string RoleName { get; set; }
    }
}
