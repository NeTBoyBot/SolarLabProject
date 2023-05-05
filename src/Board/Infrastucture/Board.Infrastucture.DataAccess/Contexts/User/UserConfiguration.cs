using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Board.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Board.Infrastucture.DataAccess.Contexts.User
{
    public class UserConfiguration : IEntityTypeConfiguration<Domain.User>
    {
        public void Configure(EntityTypeBuilder<Domain.User> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.UserName).HasMaxLength(256).IsRequired();
            builder.Property(a => a.Password).HasMaxLength(50).IsRequired();
            builder.Property(a => a.CreationTime).HasConversion(s => s, s => DateTime.SpecifyKind(s, DateTimeKind.Utc));

            builder.HasMany(u => u.Photos).WithOne(p => p.User).HasForeignKey(f => f.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u=>u.SendedMessages).WithOne(m=>m.Sender).HasForeignKey(f=>f.SenderId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u=>u.RecievedMessages).WithOne(m=>m.Reciever).HasForeignKey(f=>f.RecieverId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u => u.FavoriteAds).WithOne(f => f.User).HasForeignKey(f => f.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u=>u.Ads).WithOne(a=>a.Owner).HasForeignKey(f=>f.OwnerId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u=>u.RecievedComments).WithOne(c=>c.User).HasForeignKey(f=>f.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u=>u.SendedComments).WithOne(c=>c.Sender).HasForeignKey(f=>f.SenderId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(u => u.Role).WithMany().HasForeignKey(f => f.Id);
        }
    }
}
