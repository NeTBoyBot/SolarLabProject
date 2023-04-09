using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Infrastucture.DataAccess.Contexts.Ad
{
    public class AdConfiguration : IEntityTypeConfiguration<Domain.Ad>
    {
        public void Configure(EntityTypeBuilder<Domain.Ad> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Desc).HasMaxLength(256).IsRequired();
            builder.Property(a => a.Name).HasMaxLength(50).IsRequired();
            builder.Property(a => a.Price).IsRequired();
        }
    }
}
