using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Infrastucture.DataAccess.Contexts.Photo.UserPhoto
{
    public class UserPhotoConfiguration : IEntityTypeConfiguration<Domain.Photos.UserPhoto>
    {
        public void Configure(EntityTypeBuilder<Domain.Photos.UserPhoto> builder)
        {
            builder.HasKey(a => a.Id);
            builder.HasOne(p => p.User).WithMany(p => p.Photos).HasForeignKey(u=>u.UserId);

        }
    }
}
