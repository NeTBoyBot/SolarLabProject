using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Infrastucture.DataAccess.Contexts.Photo.AdPhoto
{
    public class AdPhotoConfiguration : IEntityTypeConfiguration<Domain.Photos.AdPhoto>
    {
        public void Configure(EntityTypeBuilder<Domain.Photos.AdPhoto> builder)
        {
            builder.HasKey(a => a.Id);
            builder.HasOne(p => p.Ad).WithMany(p => p.Photos).HasForeignKey(u => u.AdId);
        }
    }
}
