using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Infrastucture.DataAccess.Contexts.Role
{
    public class RoleConfiguration : IEntityTypeConfiguration<Domain.Role>
    {
        public void Configure(EntityTypeBuilder<Domain.Role> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.RoleName).HasMaxLength(30).IsRequired();
        }
    }
}
