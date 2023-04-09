using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Infrastucture.DataAccess.Contexts.IdentityUser
{
    public class IdentityUserConfiguration : IEntityTypeConfiguration<Domain.IdentityUser>
    {
        public void Configure(EntityTypeBuilder<Domain.IdentityUser> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
